using System;

namespace Entity.@base
{
    [Serializable]
    public class PagerQueryParam
    {
        private int _pageindex = 0;
        private int _pagesize = 0;
        private int _totalcount = 0;
        private int _retotalcount = 0;
        private string _strwhere = string.Empty;
        private string _strparm = string.Empty;

        public PagerQueryParam()
        {
            _pageindex = 1;
            _pagesize = 15;
            _totalcount = 0;
            _retotalcount = 0;
            _strwhere = string.Empty;
        }
        public int PageIndex 
        {
            get
            {
                return _pageindex;
            }
            set
            {
                _pageindex = value;
            }
        }
        public int PageSize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = value;
            }
        }
        public int ReTotalCount
        {
            get
            {
                return _retotalcount;
            }
            set
            {
                _retotalcount = value;
            }
        }
        public string StrWhere
        {
            get
            {
                return _strwhere;
            }
            set
            {
                _strwhere = value;
            }
        }

        public int TotalCount
        {
            get
            {
                return _totalcount;
            }
            set
            {
                _totalcount = value;
            }
        }

        public string StrParm
        {
            get
            {
                return _strparm;
            }
            set
            {
                _strparm = value;
            }
        }
    }
}
