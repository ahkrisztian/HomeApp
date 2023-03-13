using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAppMAUI.Model.BaseHomeModel;

public class RoomModelBaseUI : BaseModel
{
    private int id;
    public int Id 
    {
        get { return id; }
        set
        {
            if (id == value)
            {
                return;
            }

            id = value;
            OnPropertychanged();
        }
    }

    private bool lights;
    public bool Lights {
        get { return lights; }
        set
        {
            if (lights == value)
            {
                return;
            }

            lights = value;
            OnPropertychanged();
        }
    }

    private bool windows;
    public bool Windows {
        get { return windows; }
        set
        {
            if (windows == value)
            {
                return;
            }

            windows = value;
            OnPropertychanged();
        }
    }

    private string description;
    public string Description {
        get { return description; }
        set
        {
            if (description == value)
            {
                return;
            }

            description = value;
            OnPropertychanged();
        }
    }

    private int homeModelId;
    public int HomeModelId {
        get { return homeModelId; }
        set
        {
            if (homeModelId == value)
            {
                return;
            }

            homeModelId = value;
            OnPropertychanged();
        }
    }

    private int roomTypeId;
    public int RoomTypeId {
        get { return roomTypeId; }
        set
        {
            if (roomTypeId == value)
            {
                return;
            }

            roomTypeId = value;
            OnPropertychanged();
        }
    }
}
