﻿using System;
using System.Drawing;
using System.Windows.Forms;
using YamuiFramework.Animations.Transitions;
using YamuiFramework.Controls;
using YamuiFramework.Forms;
using YamuiFramework.Themes;

namespace YamuiDemoApp {
    public partial class Form1 : YamuiForm {
        public Form1() {
            InitializeComponent();
        }

        private void yamuiTabControl1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void yamuiTabPage9_Click(object sender, EventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {
            //ApplyHideSettingGlobally(this);
        }

        private void yamuiLink6_Click(object sender, EventArgs e) {
            GoToPage("yamuiTabSecAppearance");
        }

        private void yamuiLink7_Click(object sender, EventArgs e) {
            var toastNotification = new YamuiNotifications("<img src='high_importance' />This is a notification test", 5);
            toastNotification.Show();
        }

        private void classic1_Load(object sender, EventArgs e) {

        }

        private void text1_Load(object sender, EventArgs e) {

        }

        private static bool lab = true;
        private void yamuiLink8_Click(object sender, EventArgs e) {
            statusLabel.UseCustomForeColor = true;
            statusLabel.ForeColor = ThemeManager.Current.LabelsColorsNormalForeColor;
            var t = new Transition(new TransitionType_Linear(500));
            if (lab) 
                t.add(statusLabel, "Text", "Hello world!");
            else
                t.add(statusLabel, "Text", "<b>WARNING :</b> this user is awesome");
            t.add(statusLabel, "ForeColor", ThemeManager.AccentColor);
            t.TransitionCompletedEvent += (o, args) => {
                Transition.run(statusLabel, "ForeColor", ThemeManager.Current.LabelsColorsNormalForeColor, new TransitionType_CriticalDamping(400));
            };
            t.run();
            lab = !lab;
        }
    }
}
