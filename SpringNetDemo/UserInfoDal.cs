using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    public class UserInfoDal : IUserInfoDal
    {
        public UserInfoDal(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; set; }
        public void Show()
        {
            Console.WriteLine("This is from UserInfoDal" + "(" + Name + ")");
        }
    }
}
