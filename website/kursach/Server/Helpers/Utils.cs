using FuzzyString;
using kursach.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kursach.Server.Helpers
{
    public class Utils
    {
        public bool TryCompareStrings(string source, string target)
        {
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

            // Choose which algorithms should weigh in for the comparison
            options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);
            // Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?
            FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Normal;

            // Get a boolean determination of approximate equality
            bool result = source.ApproximatelyEquals(target, options, tolerance);

            return result;
        }
        public bool TryCompareStrings(string source, string target, FuzzyStringComparisonTolerance tolerance)
        {
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

            // Choose which algorithms should weigh in for the comparison
            options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);
            // Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?

            // Get a boolean determination of approximate equality
            bool result = source.ApproximatelyEquals(target, options, tolerance);

            return result;
        }
        //public Session CheckSession(string sid)
        //{
        //    Session session = context.Sessions.FirstOrDefault(s => s.Id == sid);
        //    return session;
        //}

        //public bool CheckUserPermission(string cid, string pid)
        //{
        //    Customer customer = context.Customers.FirstOrDefault(c => c.Id == cid);
        //    CompanyAdmin admin = context.CompanyAdmins.FirstOrDefault(ca => ca.Customerid == cid);

        //    if (customer == null || admin == null || admin.Company.Products.Any(p => p.Id == pid))
        //        return false;
        //    return true;
        //}

        //public bool ValidateModel(object model)
        //{

        //    foreach (var prop in model.GetType().GetProperties())
        //    {
        //        if (prop.PropertyType == typeof(string) && prop.GetValue(model).ToString().Trim() == "")
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }

}
