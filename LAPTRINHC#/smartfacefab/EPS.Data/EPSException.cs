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
        [Description("Exception.Ex1")]
        DeleteRecordWithRelatedData = 1,
        [Description("{0} Exception.Ex2")]
        ExistInTheSystem = 2,
        [Description("{0} Exception.Ex3")]
        NotExistInTheSystem = 3,
        [Description("{0} đã tồn tại trong hệ thống.")]
        ExistCode = 2,
        [Description("Dữ liệu đã có sự ràng buộc, vui lòng không xóa.{0}")]
        DeleteConstraintData = 3,
    }
}
