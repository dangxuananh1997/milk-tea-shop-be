using System.Collections.Generic;

namespace Core.ObjectModel.Identity
{
    public class SystemIdentityResult
    {
        private List<string> _errors;

        public List<string> Errors
        {
            get
            {
                if (this._errors == null)
                {
                    this._errors = new List<string>();
                }

                return this._errors;
            }
        }

        public bool IsError
        {
            get
            {
                return this.Errors.Count > 0;
            }
        }

        public object Data { get; set; }
    }
}
