﻿using System;

namespace YamuiFramework.Helper {
    public class WindowWrapper : System.Windows.Forms.IWin32Window {
        public WindowWrapper(IntPtr handle) {
            _hwnd = handle;
        }

        public IntPtr Handle {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
}
