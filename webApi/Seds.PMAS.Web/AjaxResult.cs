using System.Collections.Generic;

namespace Seds.PMAS.Web
{
    public class AjaxResult<T> where T : new()
    {
        public AjaxResult()
        {
            this.Errors = new List<string>();
        }

        public T ItemResult { get; set; }

        public List<string> Errors { get; set; }

        public bool IsValid { get { return Errors.Count == 0; } }

        private int _index;
        public int Index
        {
            get { return this._index; }
            set { _index = value; }
        }

        private int _skip;

        public int Skip
        {
            get { return this._skip; }
            set { _skip = value; }
        }

        private int _take;

        public int Take
        {
            get { return this._take == 0 ? 10 : this._take; }
            set { _take = value; }
        }

        public int TotalRows
        {
            get;
            set;
        }

    }
}