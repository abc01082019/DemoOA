using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAP.OA.EFDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAP.OA.Model;
using MAP.OA.IDAL;

namespace MAP.OA.EFDAL.Tests
{
    /// <summary>
    /// Try not to use third-part data in unit-test to avoid misjudgment caused by third-party data, 
    /// if you used it, remember to delete it when you are finished.
    /// 1. Save debug time
    /// 2. Confident
    /// 3. Is also a design
    /// 4. Also a kind of project management
    /// </summary>
    [TestClass()]
    public class UserInfoDalTests
    {
        [TestMethod()]
        public void GetUsersTest()
        {

            IUserInfoDal dal = new UserInfoDal();


            // Insert test data
            for (var i =0; i < 10; i++)
            {
                dal.Add(new UserInfo()
                {
                    Name = "testName" + i
                });
            }

            IQueryable<UserInfo> temp = dal.GetEntities(u => true);


            Assert.AreEqual(true, temp.Count() >= 10);


            //Assert.Fail();
        }
    }
}