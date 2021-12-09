using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Helpers
{
    public class Filter
    {
        private int key;

        public int? _id { get; set; }
        public string dataGT { get; set; }
        public string dataGE { get; set; }
        public string dataLT { get; set; }
        public string dataLE { get; set; }
        public int Top { get; set; }
        public int Skip { get; set; }


        public Filter()
        {
            _id = null;
        }

        public Filter(int id)
        {
            _id = id;
            Top = 0;
            Skip = 0;
        }

        public void ParseQuery(string queryFilter, int? top = null, int? skip = null)
        {
            queryFilter = queryFilter.ToUpper();
            if (queryFilter.Contains("DATA"))
            {
                if (queryFilter.Contains("GT"))
                    dataGT = queryFilter.Substring(queryFilter.IndexOf("GT") + 3, 10);
                if (queryFilter.Contains("LT"))
                    dataLT = queryFilter.Substring(queryFilter.IndexOf("LT") + 3, 10);
                if (queryFilter.Contains("GE"))
                    dataGE = queryFilter.Substring(queryFilter.IndexOf("GE") + 3, 10);
                if (queryFilter.Contains("LE"))
                    dataLE = queryFilter.Substring(queryFilter.IndexOf("LE") + 3, 10);
            }
            if (top != null)
                Top = (int)top; //Convert.ToInt32(queryFilter.Split('&').ToList().Where(x => x.Contains("TOP")).First().Split('=').Last());
            if (skip != null)
                Skip = (int)skip; //Convert.ToInt32(queryFilter.Split('&').ToList().Where(x => x.Contains("SKIP")).First().Split('=').Last());

        }

        public void FilterView(View view)
        {
            if (_id != null)
                view.Condition = new FieldCondition.Equal("ID", _id);
            if (!String.IsNullOrEmpty(dataGE))
                view.Condition &= new FieldCondition.GreaterEqual("Data", dataGE);
            if (!String.IsNullOrEmpty(dataLE))
                view.Condition &= new FieldCondition.LessEqual("Data", dataLE);
            if (!String.IsNullOrEmpty(dataGT))
                view.Condition &= new FieldCondition.Greater("Data", dataGT);
            if (!String.IsNullOrEmpty(dataLT))
                view.Condition &= new FieldCondition.Less("Data", dataLT);

        }
    }
}