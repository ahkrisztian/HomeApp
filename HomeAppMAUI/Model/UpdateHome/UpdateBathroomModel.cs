using HomeAppMAUI.Model.BaseHomeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppMAUI.Model.UpdateHome
{
    public class UpdateBathroomModel : RoomModelBaseUI
    {
        private bool tap;
        public bool Tap {
            get { return tap; }
            set
            {
                if (tap == value)
                {
                    return;
                }

                tap = value;
                OnPropertychanged();
            }
        }

        public bool bathTap;
        public bool BathTap {
            get { return bathTap; }
            set
            {
                if (bathTap == value)
                {
                    return;
                }

                bathTap = value;
                OnPropertychanged();
            }
        }

        public bool toilet;
        public bool Toilet {
            get { return toilet; }
            set
            {
                if (toilet == value)
                {
                    return;
                }

                toilet = value;
                OnPropertychanged();
            }
        }

        public bool shower;
        public bool Shower {
            get { return shower; }
            set
            {
                if (shower == value)
                {
                    return;
                }

                shower = value;
                OnPropertychanged();
            }
        }

    }
}
