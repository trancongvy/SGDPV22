using System;
using System.Collections.Generic;
using System.Text;

namespace CDTLib
{
    public class SqlSearching
    {
        public string GenSqlFromGridFilter(string strFilter)
        {
            if (strFilter.Contains("?"))
                return string.Empty;
            if(strFilter.Contains(" Like '"))
            {
                updateLike(ref strFilter);                
            }
            if (strFilter.Contains("StartsWith(") )
            {
                UpdateStarWith(ref strFilter);
            }
            if (strFilter.Contains("EndsWith("))
            {
                UpdateEndWith(ref strFilter);
            }
            if (strFilter.Contains("Contains("))
            {
                int start = strFilter.IndexOf("Contains(");
                int end = 0;
                for (int i = start; i < strFilter.Length; i++)
                {
                    if (strFilter[i] == ')')
                    {
                        end = i;
                        string s = strFilter.Substring(start, end - start + 1);
                        string[] sl;
                        string r = s.Replace("Contains(", "");
                        r = r.Replace(")", "");
                        sl = r.Split(",".ToCharArray());
                        r = sl[0] + " like '%" + sl[1].Trim().Replace("'", "") + "%' ";
                        strFilter = strFilter.Replace(s, r);
                    }
                }
            }
            strFilter = strFilter.Replace("#", "'");
            strFilter = strFilter.Replace("{", "'");
            strFilter = strFilter.Replace("}", "'");
            strFilter = strFilter.Replace("True", "1");
            strFilter = strFilter.Replace("False", "0");
            strFilter = strFilter.Replace("0m", "0");
            if (strFilter.Contains("IsOutlookIntervalNextWeek"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalNextWeek", "1 = dbo.IsOutlookIntervalNextWeek");
            }
            if (strFilter.Contains("IsOutlookIntervalBeyondThisYear"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalBeyondThisYear", "1 = dbo.IsOutlookIntervalBeyondThisYear");
            }
            if (strFilter.Contains("IsOutlookIntervalLaterThisYear"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalLaterThisYear", "1 = dbo.IsOutlookIntervalBeyondThisYear");
            }
            if (strFilter.Contains("IsOutlookIntervalLaterThisWeek"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalLaterThisWeek", "1 = dbo.IsOutlookIntervalLaterThisWeek");
            }
            if (strFilter.Contains("IsOutlookIntervalTomorrow"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalTomorrow", "1 = dbo.IsOutlookIntervalTomorrow");
            }
            if (strFilter.Contains("IsOutlookIntervalToday"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalToday", "1 = dbo.IsOutlookIntervalToday");
            }
            if (strFilter.Contains("IsOutlookIntervalYesterday"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalYesterday", "1 = dbo.IsOutlookIntervalYesterday");
            }
            if (strFilter.Contains("IsOutlookIntervalEarlierThisWeek"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalEarlierThisWeek", "1 = dbo.IsOutlookIntervalEarlierThisWeek");
            }
            if (strFilter.Contains("IsOutlookIntervalLastWeek"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalLastWeek", "1 = dbo.IsOutlookIntervalLastWeek");
            }
            if (strFilter.Contains("IsOutlookIntervalLaterThisMonth"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalLaterThisMonth", "1 = dbo.IsOutlookIntervalLaterThisMonth");
            }
            if (strFilter.Contains("IsOutlookIntervalEarlierThisMonth"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalEarlierThisMonth", "1 = dbo.IsOutlookIntervalEarlierThisMonth");
            }
            if (strFilter.Contains("IsOutlookIntervalEarlierThisYear"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalEarlierThisYear", "1 = dbo.IsOutlookIntervalEarlierThisYear");
            }
            if (strFilter.Contains("IsOutlookIntervalPriorThisYear"))
            {
                strFilter = strFilter.Replace("IsOutlookIntervalPriorThisYear", "1 = dbo.IsOutlookIntervalPriorThisYear");
            }
            if (strFilter.Contains("IsSameDay"))
            {
                strFilter = strFilter.Replace("IsSameDay", "1 = dbo.IsSameDay");
            }
            if (strFilter.Contains("IsThisMonth"))
            {
                strFilter = strFilter.Replace("IsThisMonth", "1 = dbo.IsThisMonth");
            }
            if (strFilter.Contains("IsThisWeek"))
            {
                strFilter = strFilter.Replace("IsThisWeek", "1 = dbo.IsThisWeek");
            }
            if (strFilter.Contains("IsThisYear"))
            {
                strFilter = strFilter.Replace("IsThisYear", "1 = dbo.IsThisYear");
            }
            
            if (strFilter.Contains("Between"))
                UpdateBetweenOperator(ref strFilter);
            
            return (strFilter);
        }
        private void updateLike(ref string strFilter)
        {
            int start = strFilter.IndexOf(" Like '");
            int end = 0;
            for (int i = start + 7; i < strFilter.Length; i++)
            {
                if (strFilter[i].ToString() == "'")
                {
                    end = i;
                    string s = strFilter.Substring(start + 7, end - (start + 7));
                    string S = s.ToUpper();
                    strFilter = strFilter.Replace(s, "%" + S + "%");
                    break;
                }
            }
            if (strFilter.Contains(" Like '"))
            {
                updateLike(ref strFilter);
            }
        }
        private void UpdateEndWith(ref string strFilter)
        {

            int start = strFilter.IndexOf("EndsWith(");
            int end = 0;
            for (int i = start; i < strFilter.Length; i++)
            {
                if (strFilter[i] == ')')
                {
                    end = i;
                    string s = strFilter.Substring(start, end - start +1);
                    string[] sl;
                    string r = s.Replace("EndsWith(", "");
                    r = r.Replace(")", "");
                    sl = r.Split(",".ToCharArray());
                    r = sl[0] + " like '%" + sl[1].Trim().Replace("'", "") + "' ";
                    strFilter = strFilter.Replace(s, r);

                    break;
                }
            }
            if (strFilter.Contains("EndsWith("))
                UpdateEndWith(ref strFilter);
        }
        private void UpdateStarWith(ref string strFilter)
        {
            
            int start = strFilter.IndexOf("StartsWith(");
            int end = 0;
            for (int i = start; i < strFilter.Length; i++)
            {
                if (strFilter[i] == ')')
                {
                    end = i;
                    string s = strFilter.Substring(start, end - start + 1);
                    string[] sl;
                    string r = s.Replace("StartsWith(", "");
                    r = r.Replace(")", "");
                    sl = r.Split(",".ToCharArray());
                    r = sl[0] + " like '" + sl[1].Trim().Replace("'","") + "%' ";
                    strFilter = strFilter.Replace(s, r);
                    
                    break;
                }
            }
            if (strFilter.Contains("StartsWith("))
                UpdateStarWith(ref strFilter);
        }
        private void UpdateBetweenOperator(ref string strFilter)
        {
            int bIndex = strFilter.IndexOf("Between");
            int bStart = -1, bEnd = -1;
            for (int i = bIndex; i >= 0; i--)
                if (strFilter[i] == '[')
                {
                    bStart = i;
                    break;
                }
            for (int i = bIndex; i < strFilter.Length; i++)
                if (strFilter[i] == ')')
                {
                    bEnd = i;
                    break;
                }
            if (bStart >= 0 && bEnd >= 0)
            {
                string bString = strFilter.Substring(bStart, bEnd - bStart + 1);
                string bStringNew = bString.Replace("Between", "between");
                bStringNew = bStringNew.Replace("(", " ");
                bStringNew = bStringNew.Replace(",", " and ");
                bStringNew = "(" + bStringNew;
                strFilter = strFilter.Replace(bString, bStringNew);
            }
            if (strFilter.Contains("Between"))
                UpdateBetweenOperator(ref strFilter);
        }
    }
}
