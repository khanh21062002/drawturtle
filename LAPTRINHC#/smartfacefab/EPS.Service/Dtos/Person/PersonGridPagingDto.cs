using EPS.Service.Models;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Person
{
    public class PersonGridPagingDto : PagingParams<PersonGridDto>
    {
        public string FilterText { get; set; }
        public string Address { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> Gender { get; set; }
        public string FileData { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> filterType { get; set; }
        public Nullable<int> CountType { get; set; }
        public Nullable<int> CountNumber { get; set; }
        public string[] personTypeArray { get; set; }
        public string PersonId { get; set; }
        public double ValueThreshold { get; set; }

        public override List<Expression<Func<PersonGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);

            if (FileData != null && string.IsNullOrEmpty(PersonId))
            {
                predicates.Add(x => x.Gender == 8);
            }
            if (!string.IsNullOrEmpty(PersonId))
            {
                List<Guid> lstperId = PersonId.Split(';').Select(Guid.Parse).ToList();
                predicates.Add(x => lstperId.Contains(x.PersonId));
            }

            //Tìm kiếm theo tên khách
            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.FullName.Contains(FilterText.Trim()));
            }

            //Tìm kiếm theo địa chỉ
            if (!string.IsNullOrEmpty(Address))
            {
                predicates.Add(x => x.HomeAddress.Contains(Address.Trim()));
            }

            //Tìm kiếm theo đơn vị Id
            if (CompId.HasValue)
            {
                predicates.Add(x => x.CompId == CompId);
            }

            //Tìm kiếm theo loại khách
            if (filterType.HasValue)
            {
                predicates.Add(x => x.PersonType == filterType);
            }

            //Tìm kiếm theo giới tính
            if (Gender.HasValue)
            {
                predicates.Add(x => x.Gender == Gender);
            }

            //Tìm kiếm theo số điện thoại
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                predicates.Add(x => x.PhoneNumber.Contains(PhoneNumber.Trim()));
            }

            //Tìm kiếm theo số lần đến
            if (CountType.HasValue && CountNumber.HasValue)
            {
                if (CountType.Value == 1)
                {
                    predicates.Add(x => x.NumberOfTimes > CountNumber.Value);
                }
                if (CountType == 2)
                {
                    predicates.Add(x => x.NumberOfTimes < CountNumber.Value);
                }
                if (CountType == 3)
                {
                    predicates.Add(x => x.NumberOfTimes == CountNumber.Value);
                }
            }

            if (personTypeArray != null && personTypeArray.Length > 0)
            {
                predicates.Add(x => personTypeArray.Select(int.Parse).ToList().Contains((int)x.PersonType));
            }

            return predicates;
        }
    }
}
