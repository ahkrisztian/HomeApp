using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAppMAUI.Model;

public class NewHomeModel : BaseModel
{
		private int userid;

		public int UserId
        {
			get { return userid; }
			set 
			{ 
				if(userid == value)
				{
					return;
				}

            userid = value;
            OnPropertychanged();

            }
		}

    private int addressId;

    public int AddressId
    {
        get { return addressId; }
        set
        {
            if (addressId == value)
            {
                return;
            }

            addressId = value;
            OnPropertychanged();
        }
    }

    private string _name;

    public string Name
    {
        get { return _name; }
        set
        {
            if (_name == value)
            {
                return;
            }

            _name = value;
            OnPropertychanged();
        }
    }

    private string _description;

    public string Description
    {
        get { return _description; }
        set
        {
            if (_description == value)
            {
                return;
            }

            _description = value;
            OnPropertychanged();
        }
    }

}
