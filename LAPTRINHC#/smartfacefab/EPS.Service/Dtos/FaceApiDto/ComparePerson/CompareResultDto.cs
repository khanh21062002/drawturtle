using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.ComparePerson
{
    public class CompareResultDto
    {
        public InputDataCompareDto input { get; set; }
        public List<ResultCompareDto> result { get; set; }
    }
}
