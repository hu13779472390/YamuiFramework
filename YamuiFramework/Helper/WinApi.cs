﻿#region header
// ========================================================================
// Copyright (c) 2016 - Julien Caillon (julien.caillon@gmail.com)
// This file (WinApi.cs) is part of YamuiFramework.
// 
// YamuiFramework is a free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// YamuiFramework is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with YamuiFramework. If not, see <http://www.gnu.org/licenses/>.
// ========================================================================
#endregion
using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace YamuiFramework.Helper {

    [SuppressUnmanagedCodeSecurity]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class WinApi {
        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT {
            public Int32 x;
            public Int32 y;

            public POINT(Int32 x, Int32 y) { this.x = x; this.y = y; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE {
            public Int32 cx;
            public Int32 cy;

            public SIZE(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TCHITTESTINFO {
            public Point pt;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public RECT(int left, int top, int right, int bottom) {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }
        #endregion

        #region Enums
        public enum ScrollBar {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        public enum HitTest {
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTGROWBOX = 4,
            HTSIZE = HTGROWBOX,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTREDUCE = HTMINBUTTON,
            HTZOOM = HTMAXBUTTON,
            HTSIZEFIRST = HTLEFT,
            HTSIZELAST = HTBOTTOMRIGHT,
            HTTRANSPARENT = -1
        }

        public enum TabControlHitTest {
            TCHT_NOWHERE = 1
        }

        public enum Messages : uint {
            WM_NULL = 0x0,
            WM_CREATE = 0x1,
            WM_DESTROY = 0x2,
            WM_MOVE = 0x3,
            WM_SIZE = 0x5,
            WM_ACTIVATE = 0x6,
            WM_SETFOCUS = 0x7,
            WM_KILLFOCUS = 0x8,
            WM_ENABLE = 0xa,
            WM_SETREDRAW = 0xb,
            WM_SETTEXT = 0xc,
            WM_GETTEXT = 0xd,
            WM_GETTEXTLENGTH = 0xe,
            WM_PAINT = 0xf,
            WM_CLOSE = 0x10,
            WM_QUERYENDSESSION = 0x11,
            WM_QUERYOPEN = 0x13,
            WM_ENDSESSION = 0x16,
            WM_QUIT = 0x12,
            WM_ERASEBKGND = 0x14,
            WM_SYSCOLORCHANGE = 0x15,
            WM_SHOWWINDOW = 0x18,
            WM_WININICHANGE = 0x1a,
            WM_SETTINGCHANGE = WM_WININICHANGE,
            WM_DEVMODECHANGE = 0x1b,
            WM_ACTIVATEAPP = 0x1c,
            WM_FONTCHANGE = 0x1d,
            WM_TIMECHANGE = 0x1e,
            WM_CANCELMODE = 0x1f,
            WM_SETCURSOR = 0x20,
            WM_MOUSEACTIVATE = 0x21,
            WM_CHILDACTIVATE = 0x22,
            WM_QUEUESYNC = 0x23,
            WM_GETMINMAXINFO = 0x24,
            WM_PAINTICON = 0x26,
            WM_ICONERASEBKGND = 0x27,
            WM_NEXTDLGCTL = 0x28,
            WM_SPOOLERSTATUS = 0x2a,
            WM_DRAWITEM = 0x2b,
            WM_MEASUREITEM = 0x2c,
            WM_DELETEITEM = 0x2d,
            WM_VKEYTOITEM = 0x2e,
            WM_CHARTOITEM = 0x2f,
            WM_SETFONT = 0x30,
            WM_GETFONT = 0x31,
            WM_SETHOTKEY = 0x32,
            WM_GETHOTKEY = 0x33,
            WM_QUERYDRAGICON = 0x37,
            WM_COMPAREITEM = 0x39,
            WM_GETOBJECT = 0x3d,
            WM_COMPACTING = 0x41,
            WM_COMMNOTIFY = 0x44,
            WM_WINDOWPOSCHANGING = 0x46,
            WM_WINDOWPOSCHANGED = 0x47,
            WM_POWER = 0x48,
            WM_COPYDATA = 0x4a,
            WM_CANCELJOURNAL = 0x4b,
            WM_NOTIFY = 0x4e,
            WM_INPUTLANGCHANGEREQUEST = 0x50,
            WM_INPUTLANGCHANGE = 0x51,
            WM_TCARD = 0x52,
            WM_HELP = 0x53,
            WM_USERCHANGED = 0x54,
            WM_NOTIFYFORMAT = 0x55,
            WM_CONTEXTMENU = 0x7b,
            WM_STYLECHANGING = 0x7c,
            WM_STYLECHANGED = 0x7d,
            WM_DISPLAYCHANGE = 0x7e,
            WM_GETICON = 0x7f,
            WM_SETICON = 0x80,
            WM_NCCREATE = 0x81,
            WM_NCDESTROY = 0x82,
            WM_NCCALCSIZE = 0x83,
            WM_NCHITTEST = 0x84,
            WM_NCPAINT = 0x85,
            WM_NCACTIVATE = 0x86,
            WM_GETDLGCODE = 0x87,
            WM_SYNCPAINT = 0x88,
            WM_NCMOUSEMOVE = 0xa0,
            WM_NCLBUTTONDOWN = 0xa1,
            WM_NCLBUTTONUP = 0xa2,
            WM_NCLBUTTONDBLCLK = 0xa3,
            WM_NCRBUTTONDOWN = 0xa4,
            WM_NCRBUTTONUP = 0xa5,
            WM_NCRBUTTONDBLCLK = 0xa6,
            WM_NCMBUTTONDOWN = 0xa7,
            WM_NCMBUTTONUP = 0xa8,
            WM_NCMBUTTONDBLCLK = 0xa9,
            WM_NCXBUTTONDOWN = 0xab,
            WM_NCXBUTTONUP = 0xac,
            WM_NCXBUTTONDBLCLK = 0xad,
            WM_INPUT = 0xff,
            WM_KEYFIRST = 0x100,
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WM_CHAR = 0x102,
            WM_DEADCHAR = 0x103,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105,
            WM_SYSCHAR = 0x106,
            WM_SYSDEADCHAR = 0x107,
            WM_UNICHAR = 0x109,
            WM_KEYLAST = 0x108,
            WM_IME_STARTCOMPOSITION = 0x10d,
            WM_IME_ENDCOMPOSITION = 0x10e,
            WM_IME_COMPOSITION = 0x10f,
            WM_IME_KEYLAST = 0x10f,
            WM_INITDIALOG = 0x110,
            WM_COMMAND = 0x111,
            WM_SYSCOMMAND = 0x112,
            WM_TIMER = 0x113,
            WM_HSCROLL = 0x114,
            WM_VSCROLL = 0x115,
            WM_INITMENU = 0x116,
            WM_INITMENUPOPUP = 0x117,
            WM_MENUSELECT = 0x11f,
            WM_MENUCHAR = 0x120,
            WM_ENTERIDLE = 0x121,
            WM_MENURBUTTONUP = 0x122,
            WM_MENUDRAG = 0x123,
            WM_MENUGETOBJECT = 0x124,
            WM_UNINITMENUPOPUP = 0x125,
            WM_MENUCOMMAND = 0x126,
            WM_CHANGEUISTATE = 0x127,
            WM_UPDATEUISTATE = 0x128,
            WM_QUERYUISTATE = 0x129,
            WM_CTLCOLOR = 0x19,
            WM_CTLCOLORMSGBOX = 0x132,
            WM_CTLCOLOREDIT = 0x133,
            WM_CTLCOLORLISTBOX = 0x134,
            WM_CTLCOLORBTN = 0x135,
            WM_CTLCOLORDLG = 0x136,
            WM_CTLCOLORSCROLLBAR = 0x137,
            WM_CTLCOLORSTATIC = 0x138,
            WM_MOUSEFIRST = 0x200,
            WM_MOUSEMOVE = 0x200,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_LBUTTONDBLCLK = 0x203,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_RBUTTONDBLCLK = 0x206,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x208,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MOUSEWHEEL = 0x20a,
            WM_XBUTTONDOWN = 0x20b,
            WM_XBUTTONUP = 0x20c,
            WM_XBUTTONDBLCLK = 0x20d,
            WM_MOUSELAST = 0x20d,
            WM_PARENTNOTIFY = 0x210,
            WM_ENTERMENULOOP = 0x211,
            WM_EXITMENULOOP = 0x212,
            WM_NEXTMENU = 0x213,
            WM_SIZING = 0x214,
            WM_CAPTURECHANGED = 0x215,
            WM_MOVING = 0x216,
            WM_POWERBROADCAST = 0x218,
            WM_DEVICECHANGE = 0x219,
            WM_MDICREATE = 0x220,
            WM_MDIDESTROY = 0x221,
            WM_MDIACTIVATE = 0x222,
            WM_MDIRESTORE = 0x223,
            WM_MDINEXT = 0x224,
            WM_MDIMAXIMIZE = 0x225,
            WM_MDITILE = 0x226,
            WM_MDICASCADE = 0x227,
            WM_MDIICONARRANGE = 0x228,
            WM_MDIGETACTIVE = 0x229,
            WM_MDISETMENU = 0x230,
            WM_ENTERSIZEMOVE = 0x231,
            WM_EXITSIZEMOVE = 0x232,
            WM_DROPFILES = 0x233,
            WM_MDIREFRESHMENU = 0x234,
            WM_IME_SETCONTEXT = 0x281,
            WM_IME_NOTIFY = 0x282,
            WM_IME_CONTROL = 0x283,
            WM_IME_COMPOSITIONFULL = 0x284,
            WM_IME_SELECT = 0x285,
            WM_IME_CHAR = 0x286,
            WM_IME_REQUEST = 0x288,
            WM_IME_KEYDOWN = 0x290,
            WM_IME_KEYUP = 0x291,
            WM_MOUSEHOVER = 0x2a1,
            WM_MOUSELEAVE = 0x2a3,
            WM_NCMOUSELEAVE = 0x2a2,
            WM_WTSSESSION_CHANGE = 0x2b1,
            WM_TABLET_FIRST = 0x2c0,
            WM_TABLET_LAST = 0x2df,
            WM_CUT = 0x300,
            WM_COPY = 0x301,
            WM_PASTE = 0x302,
            WM_CLEAR = 0x303,
            WM_UNDO = 0x304,
            WM_RENDERFORMAT = 0x305,
            WM_RENDERALLFORMATS = 0x306,
            WM_DESTROYCLIPBOARD = 0x307,
            WM_DRAWCLIPBOARD = 0x308,
            WM_PAINTCLIPBOARD = 0x309,
            WM_VSCROLLCLIPBOARD = 0x30a,
            WM_SIZECLIPBOARD = 0x30b,
            WM_ASKCBFORMATNAME = 0x30c,
            WM_CHANGECBCHAIN = 0x30d,
            WM_HSCROLLCLIPBOARD = 0x30e,
            WM_QUERYNEWPALETTE = 0x30f,
            WM_PALETTEISCHANGING = 0x310,
            WM_PALETTECHANGED = 0x311,
            WM_HOTKEY = 0x312,
            WM_PRINT = 0x317,
            WM_PRINTCLIENT = 0x318,
            WM_APPCOMMAND = 0x319,
            WM_THEMECHANGED = 0x31a,
            WM_HANDHELDFIRST = 0x358,
            WM_HANDHELDLAST = 0x35f,
            WM_AFXFIRST = 0x360,
            WM_AFXLAST = 0x37f,
            WM_PENWINFIRST = 0x380,
            WM_PENWINLAST = 0x38f,
            WM_USER = 0x400,
            WM_REFLECT = 0x2000,
            WM_APP = 0x8000,
            WM_DWMCOMPOSITIONCHANGED = 0x031E,

            SC_MOVE = 0xF010,
            SC_MINIMIZE = 0XF020,
            SC_MAXIMIZE = 0xF030,
            SC_RESTORE = 0xF120
        }
        #endregion

        #region Fields

        public const int Autohide = 0x0000001;
        public const int AlwaysOnTop = 0x0000002;

        public const Int32 MfByposition = 0x400;
        public const Int32 MfRemove = 0x1000;

        public const int TCM_HITTEST = 0x1313;

        public const Int32 ULW_COLORKEY = 0x00000001;
        public const Int32 ULW_ALPHA = 0x00000002;
        public const Int32 ULW_OPAQUE = 0x00000004;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;

        // GetWindow() constants
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
        public const int HC_ACTION = 0;
        public const int WH_CALLWNDPROC = 4;
        public const int GWL_WNDPROC = -4;

        /* used for the "always on top" function */
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        // Changes the client size of a control
        public const int EM_SETRECT = 0xB3;
        #endregion

        #region API Calls

        [DllImport("user32.dll")]
        public static extern long GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        public static Rectangle GetWindowRect(IntPtr hWnd) {
            Rectangle output = new Rectangle();
            GetWindowRect(hWnd, ref output);
            return output;
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition() {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return new Point(lpPoint.x, lpPoint.y);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        // Changes the client size of a control
        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern int SendMessageRefRect(IntPtr hWnd, uint msg, int wParam, ref RECT rect);


        // will be used to set a window to always stay on top
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        /// <summary>
        /// Gets the handle of the window that currently has focus.
        /// </summary>
        /// <returns>
        /// The handle of the window that currently has focus.
        /// </returns>
        [DllImport("user32")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Activates the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// The handle of the window to be focused.
        /// </param>
        /// <returns>
        /// True if the window was focused; False otherwise.
        /// </returns>
        [DllImport("user32")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr wnd, int msg, bool param, int lparam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int bar, int cmd);
        #endregion
    }
}
