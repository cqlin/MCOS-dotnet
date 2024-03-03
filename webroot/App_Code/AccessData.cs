using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for AccessData
/// </summary>
public class AccessData
{
    BarcodeDataDataContext db = new BarcodeDataDataContext();
    public AccessData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    static string connStr = ConfigurationManager.ConnectionStrings["MCOSConnectionString"].ConnectionString; 

    public bool UpdateBalance(int fid, decimal amount)
    {
        int ret; 
        
        using (SqlConnection cn = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand("UpdateBalance_SP", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FID", SqlDbType.Int).Value = fid;            
            cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amount;
    
            cmd.Parameters.Add("@UpdateResult", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            cn.Open();

            cmd.ExecuteNonQuery();
            ret = int.Parse(cmd.Parameters["@UpdateResult"].Value.ToString());
        }

        if (ret == 1)
        {
            return true;
        }
        else
        {
            return false;
        } 
    }

    public void getParamValueByParamName(List<Param> PList, string strpName, out string strpValue)
    {
        List<Param> ParamList = new List<Param>();
        ParamList = PList;

        int index = ParamList.FindIndex(item => item.PARAM_NAME == strpName);
        if (index >= 0)
        {
            strpValue = ParamList[index].PARAM_VALUE;
        }
        else
            strpValue = null;
    }

    public Param SelectFirstParameter()
    {
        try
        {
            Param first = (from c in db.Params
                           select c).FirstOrDefault();
            return first;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }

    public object SelectAllParameters()
    {
        try
        {
            var pList = (from c in db.Params
                         select c).ToList();
            return pList;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }

    

    public object SelectAllMenu()
    {
        try
        {
            var menuList = (from c in db.MenuItems
                            where ((c.ItemStartDate.Value <= DateTime.Now) && (c.ItemEndDate >= DateTime.Now))
                            select new
                            {
                                ID = c.ItemID,
                                Code = c.Itemcode,
                                Description = c.ItemDescription,
                                UnitPrice = c.ItemPrice,
                                Quanity = 1,
                                Subtotal = c.ItemPrice
                            }
                           ).ToList();
            return menuList;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }

    public void InsertOrderDetailList(List<OrderDetail> odList)
    {
        try
        {
        db.OrderDetails.InsertAllOnSubmit(odList);
        db.SubmitChanges();
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);            
        }
    }

    public bool IsMemberExistByMemberCode(string mCode)
    {
        try
        {
            bool existed = (from c in db.Members
                            where c.MemberCode == mCode
                            select c).Any();
            return existed;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public bool IsMemberExistByMemberID(int memberID)
    {
        try
        {
            bool existed = (from c in db.Members
                            where c.MEMBER_ID == memberID
                            select c).Any();
            return existed;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public Member GetMember(string memberCode)
    {
        Member member = new Member();

        if (IsMemberExistByMemberCode(memberCode))
        {
            member = (from c in db.Members
                      where c.MemberCode == memberCode
                      select c).FirstOrDefault();
        }
        return member;
    }

    public Member GetMemberByID(int memberID)
    {
        Member member = new Member();

        if (IsMemberExistByMemberID(memberID))
        {
            member = (from c in db.Members
                      where c.MEMBER_ID == memberID
                      select c).FirstOrDefault();
        }
        return member;
    }
 

    public object SelectMemberFamily(int mID)
    {
        try
        {
        var myQuery = from c in db.Members
                      where (c.MEMBER_ID == mID)
                      select new
                      {
                          CustomerName = String.Format("{0} {1}", c.FIRST_NAME, c.LAST_NAME),
                          c.EMAIL,                         
                          c.Family.Balance
                      };
        return myQuery;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }


    public object SelectOrderJoinMemberByOID(int oID)
    {
        try
        {
            var myQuery = from c in db.Orders
                          where (c.OrderID == oID)
                          select new
                          {
                              c.OrderID,
                              OrderDate = c.CREATE_DATE,
                              CustomerName = String.Format("{0} {1}", c.Member.FIRST_NAME, c.Member.LAST_NAME),
                              c.Member.EMAIL,                              
                              c.Member.Family.Balance,
                              c.OrderAmount                            
                          };
            return myQuery;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        } 
    }

    public object ReportSelectOrderDetailJoinMemberByOID(int oID)
    {
        try
        {
            var myQuery = from c in db.OrderDetails
                          where (c.OrderID == oID)
                          select new
                          {
                              c.OrderID,
                              ItemCode = c.MenuItem.Itemcode,
                              Description = c.MenuItem.ItemDescription,                               
                              Quantity = c.ItemQuantity 
                          };
            return myQuery;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }


    public void InsertOrder(int memberID)
    {
        Order order = new Order();
        try
        {
            order.Member_ID = memberID;

            order.CREATE_DATE = DateTime.Now;
            order.OrderDate = DateTime.Now;

            db.Orders.InsertOnSubmit(order);
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
        }
    }

    public bool IsOrderExistByMemberID(int mID)
    {
        try
        {
            bool existed = (from c in db.Orders
                            where c.Member_ID == mID
                            select c).Any();
            return existed;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public object SelectOrderbyMemberID(int mID)
    {
        if (IsOrderExistByMemberID(mID))
        {
            var order = (from c in db.Orders
                         where c.Member_ID == mID
                         orderby c.CREATE_DATE descending
                         select c).First();
            return order;
        }
        else
            return null;
    }

    public object ReportSelectAllDepositToday(DateTime dateSelected, out object sumDeposit)
    {
        try
        {
            var depositList = from c in db.DepositHistories
                         where DateTime.Compare(c.CREATE_DATE.Value.Date, dateSelected) == 0 &&
                               c.DepositAmount!= null
                         select new
                         {
                             Name = c.Family.NAME,
                             c.USERNAME,
                             c.CREATE_DATE,
                             c.DepositType,   
                             c.DepositAmount
                         };          
            sumDeposit = depositList.ToList().Select(c => c.DepositAmount).Sum();
            return depositList;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            sumDeposit = null;
            return null;
        }
    }


 //   public object ReportSelectAllOrdersByDate(DateTime dateSelected, String strState)
    public object ReportSelectAllOrdersToday(DateTime dateSelected, out object sumToday  )
    {
        try
        {
            var orders = from c in db.Orders
                         where DateTime.Compare(c.CREATE_DATE.Value.Date, dateSelected) == 0 &&
                               c.OrderAmount != null
                         select new
                          {
                              c.OrderID,
                              c.Member_ID,                              
                              MemberName = String.Format("{0} {1}", c.Member.FIRST_NAME, c.Member.LAST_NAME),                             
                              c.CREATE_DATE,
                              c.OrderAmount
                          }; 

            sumToday = orders.ToList().Select(c => c.OrderAmount).Sum();   
            return orders;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            sumToday = null;
            return null;
        }
    }

  //  public object  ReportSelectAllOrderDetailsByDate(DateTime dateSelected, String strState)
    public object ReportSelectAllOrderDetailsToday(DateTime dateSelected, out object summaryToday)
    {
        try {
        var ods =   from c in db.OrderDetails
                    where DateTime.Compare(c.Order.CREATE_DATE.Value.Date, dateSelected) == 0 &&
                           c.ItemQuantity != null
                    select new
                    {
                        c.OrderID,
                        MemberName = String.Format("{0} {1}", c.Order.Member.FIRST_NAME, c.Order.Member.LAST_NAME),    
                        c.MenuItem.ItemDescription,
                        c.ItemQuantity, 
                        c.MenuItem.ItemPrice                      
                    };                 
           // Currency = it.Decimal.ToString("C")
        summaryToday = ods.GroupBy(l => l.ItemPrice)
                         .Select(lg =>
                               new
                               {                              
                                   UnitPrice = String.Format("{0:C}", lg.Key),                                  
                                   TotalNumber = lg.Sum(w => w.ItemQuantity),                                   
                                   TotalMoney = String.Format("{0:C}",  lg.Key*lg.Sum(w => w.ItemQuantity))                                  
                               });
            return ods;
        }
         catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            summaryToday = null;
            return false;
        }
    }

    //var ListByOwner = list.GroupBy(l => l.Owner)
    //                     .Select(lg =>
    //                           new
    //                           {
    //                               Owner = lg.Key,
    //                               Boxes = lg.Count(),
    //                               TotalWeight = lg.Sum(w => w.Weight),
    //                               TotalVolume = lg.Sum(w => w.Volume)
    //                           });

    //public object ReportSelectOrderAndOrderDetailsByDate(DateTime dateSelected)
    //{ 
    //    var combineList =
    //    from ord in db.Orders
    //    join od in db.OrderDetails on ord.OrderID equals od.OrderID
    //    where DateTime.Compare(ord.CREATE_DATE.Value.Date, dateSelected) == 0 &&
    //                     ord.OrderAmount != null
    //    select new
    //    {
    //        OrderId = ord.OrderID, // or pc.ProdId
    //        MemberId = ord.Member_ID,
    //        MenuID = od.ItemID,
    //        Quantity = od.ItemQuantity,
    //        ord.OrderAmount 
    //    };

    //    return combineList;
    //}
 
    
    public object SelectOrdersByMemberIDToday(int mID)
    {
        try
        {
            var orderList = from c in db.Orders
                            where c.Member_ID == mID &&
                                  c.OrderAmount != null &&
                                  c.CREATE_DATE.Value.Year == DateTime.Today.Year &&
                                  c.CREATE_DATE.Value.Month == DateTime.Today.Month &&
                                  c.CREATE_DATE.Value.Day == DateTime.Today.Day
                                  orderby c.OrderID descending
                            select new
                            {
                                c.OrderID,
                                c.Member_ID,
                                c.CREATE_DATE,
                                c.OrderAmount
                            };
            return orderList;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }

    public List<Order> SelectAllOrdersByMemberIDToday(int mID)
    {
        if (IsOrderExistByMemberID(mID))
        {
            List<Order> orderList = (from c in db.Orders
                                     where c.Member_ID == mID &&
                                           c.OrderAmount != null &&
                                           c.CREATE_DATE.Value.Year == DateTime.Today.Year &&
                                           c.CREATE_DATE.Value.Month == DateTime.Today.Month &&
                                           c.CREATE_DATE.Value.Day == DateTime.Today.Day
                                     select c).ToList();
            return orderList;
        }
        else
            return null;
    }

    public object SelectRecentOrdersByFamilyID(int FID)
    {
        try
        {
            var orderList = (from c in db.Orders
                             join m in db.Members on c.Member_ID equals m.MEMBER_ID
                             join f in db.Families on m.FAMILY_ID equals f.FAMILY_ID
                            where f.FAMILY_ID == FID &&
                                  c.OrderAmount != null
                            orderby c.OrderID descending
                            select new
                            {
                                c.OrderID,
                                Name = m.FIRST_NAME + " " + m.LAST_NAME,
                                Date = String.Format("{0:MM/dd/yyyy hh:mm:ss tt}", c.CREATE_DATE),
                                c.OrderAmount
                            }).Take(5);
            return orderList;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }

    public void UpdateOrderByOrderID(int oID, decimal oTotal, string uName)
    {
        Order order = new Order();

        try
        {
            bool existed = (from c in db.Orders
                            where c.OrderID == oID
                            select c).Any();

            if (existed)
            {
                order = (from c in db.Orders
                         where c.OrderID == oID
                         select c).FirstOrDefault();

                order.OrderID = oID;
                order.OrderAmount = oTotal;
                order.USERNAME = uName;
                db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
        }
    }


    public List<OrderDetail> SelectOrderDetailsByOrderID(List<int> oIDList)
    {
        try
        {

            List<OrderDetail> orderDetails = (from c in db.OrderDetails
                                              where oIDList.Contains(c.OrderID)
                                              orderby c.OrderID descending
                                              select c).ToList();
            return orderDetails;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }

   

    public object SelectOrderDetailsJoinItemMenuByOID(List<int> oIDList)
    {
        try
        {
            var myQuery = from c in db.OrderDetails
                          where oIDList.Contains(c.OrderID)
                          orderby c.OrderID descending
                          select new
                          {
                              OrderID = c.OrderID,
                              ID = c.MenuItem.ItemID,
                              Code = c.MenuItem.Itemcode,
                              Description = c.MenuItem.ItemDescription,
                              Quantity = c.ItemQuantity,
                              Price = c.MenuItem.ItemPrice
                          };
            return myQuery;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return null;
        }
    }

    public bool IsFamilyIDExist(int Family_ID)
    {
        try
        {
            bool existed = (from b in db.Families
                            where b.FAMILY_ID == Family_ID
                            select b).Any();

            return existed;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return false;
        }
    }


    public void InsertDepositHistory(int Member_ID, int Family_ID, decimal dDepositAmount, string strDepositeType, string uName)
    {
        try
        {
            DepositHistory deposit = new DepositHistory();
            deposit.Family_ID = Family_ID;
            deposit.CREATE_DATE = DateTime.Now;
            deposit.DepositAmount = dDepositAmount;
            deposit.DepositType = strDepositeType;
            deposit.USERNAME = uName;
            deposit.Member_ID = Member_ID;
            db.DepositHistories.InsertOnSubmit(deposit);
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
        }
    }

    public void UpdateFamilyBalance(int fID, decimal dBalance)
    {
        try
        {
            if (IsFamilyIDExist(fID))
            {
                var aFamily = (from c in db.Families
                               where c.FAMILY_ID == fID
                               select c).FirstOrDefault();
                aFamily.Balance = dBalance;
                db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
        }
    }

    public void AddDepositToBalance(int fID, decimal dDepositAmount)
    {
        try
        {
            var aFamily = (from c in db.Families
                           where c.FAMILY_ID == fID
                           select c).FirstOrDefault();
            aFamily.Balance = aFamily.Balance + dDepositAmount;
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
        }
    }

    public object SelectRecentFamilyAccountActivitiesByFID(int FID)
    {
        var aFamily = (from c in db.DepositHistories
                       where c.Family_ID == FID
                       orderby c.CREATE_DATE descending
                      select new
                      {               
                           c.Family.NAME,
                           Date = String.Format("{0:MM/dd/yyyy hh:mm:ss tt}", c.CREATE_DATE),
                           c.DepositAmount                      
                      }).Take(5);

        return aFamily;
    }

    public bool IsFamilyExistByPhoneNumber(string phone)
    {
        try
        {
            bool existed = (from f in db.Families
                            join m in db.Members on f.FAMILY_ID equals m.FAMILY_ID
                            where (f.HOME_PHONE == phone) || (m.CELL_PHONE == phone)
                            select f).Any();
            return existed;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public Family GetFamily(string phone)
    {
        Family family = new Family();

        if (IsFamilyExistByPhoneNumber(phone))
        {
            family = (from f in db.Families
                      join m in db.Members on f.FAMILY_ID equals m.FAMILY_ID
                      where (f.HOME_PHONE == phone) || (m.CELL_PHONE == phone)
                      select f).FirstOrDefault();
        }
        return family;
    }

    public object SelectMemberListByFID(int FID)
    {
        var aMemberList = (from m in db.Members
                       where m.FAMILY_ID == FID
                       orderby m.FAMILY_ID
                       select new
                       {
                           m.FIRST_NAME,
                           m.LAST_NAME,
                           m.CHINESE_NAME,
                           m.MemberCode
                       });

        return aMemberList;
    }

}

    