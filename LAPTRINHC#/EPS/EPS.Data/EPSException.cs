using EPS.Utils.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EPS.Data
{
    public class EPSException : ApplicationException
    {
        public EPSException(EPSExceptionCode code, params object[] args) : base(string.Format(code.GetEnumDescription(), args)) { }

        public EPSException(string message) : base(message) { }
    }

    public enum EPSExceptionCode
    {
        [Description("Không thể xóa {0} do có dữ liệu liên quan.")]
        DeleteRecordWithRelatedData = 1
    }
}
