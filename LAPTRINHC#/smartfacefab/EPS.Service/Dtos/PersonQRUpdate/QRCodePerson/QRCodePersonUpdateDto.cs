﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.PersonQRUpdate.QRCodePerson
{
    public class QRCodePersonUpdateDto
    {
        public Guid Id { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CompId { get; set; }
    }
}