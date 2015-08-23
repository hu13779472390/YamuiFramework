﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YamuiFramework.Controls {
    [ToolboxBitmap(typeof(TrackBar))]
    [DefaultEvent("Scroll")]
    public class YamuiTrackBar : Control {
        #region Fields
        private int _trackerValue = 50;
        [DefaultValue(50)]
        [Category("Yamui")]
        public int Value {
            get { return _trackerValue; }
            set {
                if (value >= _barMinimum & value <= _barMaximum) {
                    _trackerValue = value;
                    OnValueChanged();
                    Invalidate();
                } else throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
            }
        }

        private int _barMinimum;
        [DefaultValue(0)]
        [Category("Yamui")]
        public int Minimum {
            get { return _barMinimum; }
            set {
                if (value < _barMaximum) {
                    _barMinimum = value;
                    if (_trackerValue < _barMinimum) {
                        _trackerValue = _barMinimum;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                } else throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
            }
        }


        private int _barMaximum = 100;
        [DefaultValue(100)]
        [Category("Yamui")]
        public int Maximum {
            get { return _barMaximum; }
            set {
                if (value > _barMinimum) {
                    _barMaximum = value;
                    if (_trackerValue > _barMaximum) {
                        _trackerValue = _barMaximum;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                } else throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
            }
        }

        private int _smallChange = 1;
        [DefaultValue(1)]
        [Category("Yamui")]
        public int SmallChange {
            get { return _smallChange; }
            set { _smallChange = value; }
        }

        private int _largeChange = 5;
        [DefaultValue(5)]
        [Category("Yamui")]
        public int LargeChange {
            get { return _largeChange; }
            set { _largeChange = value; }
        }

        private int _mouseWheelBarPartitions = 10;
        [DefaultValue(10)]
        [Category("Yamui")]
        public int MouseWheelBarPartitions {
            get { return _mouseWheelBarPartitions; }
            set {
                if (value > 0)
                    _mouseWheelBarPartitions = value;
                else throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
            }
        }

        private bool _isHovered;
        private bool _isPressed;
        private bool _isFocused;

        #endregion

        #region Constructor

        public YamuiTrackBar(int min, int max, int value) {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable |
                     ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserMouse |
                     ControlStyles.UserPaint, true);

            Minimum = min;
            Maximum = max;
            Value = value;
        }

        public YamuiTrackBar() : this(0, 100, 50) { }

        #endregion

        #region Events
        public event EventHandler ValueChanged;
        private void OnValueChanged() {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

        public event ScrollEventHandler Scroll;
        private void OnScroll(ScrollEventType scrollType, int newValue) {
            if (Scroll != null)
                Scroll(this, new ScrollEventArgs(scrollType, newValue));
        }
        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e) {
            try {
                e.Graphics.Clear(ThemeManager.FormColor.BackColor());
            } catch {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            try {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                    OnPaintBackground(e);
                OnPaintForeground(e);
            } catch {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e) {
            Color thumbColor = ThemeManager.ScrollBarsColors.ForeGround(_isFocused, _isHovered, _isPressed, Enabled);
            Color barColor = ThemeManager.ScrollBarsColors.BackGround(_isFocused, _isHovered, _isPressed, Enabled);
            DrawTrackBar(e.Graphics, thumbColor, barColor);
        }

        private void DrawTrackBar(Graphics g, Color thumbColor, Color barColor) {
            int trackX = (((_trackerValue - _barMinimum) * (Width - 6)) / (_barMaximum - _barMinimum));

            using (SolidBrush b = new SolidBrush(thumbColor)) {
                Rectangle barRect = new Rectangle(0, Height / 2 - 2, trackX, 4);
                g.FillRectangle(b, barRect);

                Rectangle thumbRect = new Rectangle(trackX, Height / 2 - 8, 6, 16);
                g.FillRectangle(b, thumbRect);
            }

            using (SolidBrush b = new SolidBrush(barColor)) {
                Rectangle barRect = new Rectangle(trackX + 7, Height / 2 - 2, Width - trackX + 7, 4);
                g.FillRectangle(b, barRect);
            }
        }

        #endregion

        #region "Managing isHovered, isPressed, isFocused"

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e) {
            _isFocused = true;
            Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e) {
            _isFocused = false;
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e) {
            _isFocused = true;
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e) {
            _isFocused = false;
            Invalidate();

            base.OnLeave(e);
        }

        #endregion

        #region Keyboard Methods

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.KeyCode == Keys.Space) {
                _isPressed = true;
                Invalidate();
            }

            base.OnKeyDown(e);
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseEnter(EventArgs e) {
            _isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            _isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            _isHovered = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        #endregion

        #endregion

        #region Keyboard Methods

        protected override void OnKeyUp(KeyEventArgs e) {
            _isPressed = false;
            Invalidate();

            base.OnKeyUp(e);

            switch (e.KeyCode) {
                case Keys.Down:
                case Keys.Left:
                    SetProperValue(Value - _smallChange);
                    OnScroll(ScrollEventType.SmallDecrement, Value);
                    break;
                case Keys.Up:
                case Keys.Right:
                    SetProperValue(Value + _smallChange);
                    OnScroll(ScrollEventType.SmallIncrement, Value);
                    break;
                case Keys.Home:
                    Value = _barMinimum;
                    break;
                case Keys.End:
                    Value = _barMaximum;
                    break;
                case Keys.PageDown:
                    SetProperValue(Value - _largeChange);
                    OnScroll(ScrollEventType.LargeDecrement, Value);
                    break;
                case Keys.PageUp:
                    SetProperValue(Value + _largeChange);
                    OnScroll(ScrollEventType.LargeIncrement, Value);
                    break;
            }

            if (Value == _barMinimum)
                OnScroll(ScrollEventType.First, Value);

            if (Value == _barMaximum)
                OnScroll(ScrollEventType.Last, Value);

            Point pt = PointToClient(Cursor.Position);
            OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, pt.X, pt.Y, 0));
        }

        protected override bool ProcessDialogKey(Keys keyData) {
            if (keyData == Keys.Tab | ModifierKeys == Keys.Shift)
                return base.ProcessDialogKey(keyData);
            OnKeyDown(new KeyEventArgs(keyData));
            return true;
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                _isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left) {
                Capture = true;
                OnScroll(ScrollEventType.ThumbTrack, _trackerValue);
                OnValueChanged();
                OnMouseMove(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (Capture & e.Button == MouseButtons.Left) {
                ScrollEventType set = ScrollEventType.ThumbPosition;
                Point pt = e.Location;
                int p = pt.X;

                float coef = (_barMaximum - _barMinimum) / (float)(ClientSize.Width - 3);
                _trackerValue = (int)(p * coef + _barMinimum);

                if (_trackerValue <= _barMinimum) {
                    _trackerValue = _barMinimum;
                    set = ScrollEventType.First;
                } else if (_trackerValue >= _barMaximum) {
                    _trackerValue = _barMaximum;
                    set = ScrollEventType.Last;
                }

                OnScroll(set, _trackerValue);
                OnValueChanged();

                Invalidate();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);
            int v = e.Delta / 120 * (_barMaximum - _barMinimum) / _mouseWheelBarPartitions;
            SetProperValue(Value + v);
        }

        #endregion

        #region Overridden Methods

        protected override void OnEnabledChanged(EventArgs e) {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        #endregion

        #region Helper Methods

        private void SetProperValue(int val) {
            if (val < _barMinimum) Value = _barMinimum;
            else if (val > _barMaximum) Value = _barMaximum;
            else Value = val;
        }

        #endregion
    }
}
