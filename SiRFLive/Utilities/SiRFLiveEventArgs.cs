﻿namespace SiRFLive.Utilities
{
    using System;

    public class SiRFLiveEventArgs : EventArgs
    {
        private bool m_state;

        public SiRFLiveEventArgs(bool state)
        {
            this.m_state = state;
        }

        public bool GetState
        {
            get
            {
                return this.m_state;
            }
        }
    }
}

