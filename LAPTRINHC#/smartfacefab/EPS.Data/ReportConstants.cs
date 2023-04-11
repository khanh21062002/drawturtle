using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data
{
    public class ReportConstants
    {
        /// <summary>
        /// Loại tài sản sở hữu toàn dân
        /// </summary>
        public enum PublicAssetType
        {
            /// <summary>
            /// Đất
            /// </summary>
            Land = 1,
            /// <summary>
            /// Nhà
            /// </summary>
            House = 2,
            /// <summary>
            /// Ô tô
            /// </summary>
            Vehicle = 3,
            /// <summary>
            /// Tài sản cố định khác
            /// </summary>
            Other = 4
        }

        /// <summary>
        /// Loại tài sản của đơn vị
        /// </summary>
        public enum OrganizationAssetType
        {
            /// <summary>
            /// Đất
            /// </summary>
            Land = 1,
            /// <summary>
            /// Nhà
            /// </summary>
            House = 2,
            /// <summary>
            /// Ô tô
            /// </summary>
            Vehicle = 3,
            /// <summary>
            /// Tài sản khác có nguyên giá trên 500 triệu
            /// </summary>
            OtherAbove500m = 4,
            /// <summary>
            /// Tài sản khác có nguyên giá dưới 500 triệu
            /// </summary>
            OtherBelow500m = 5
        }

        /// <summary>
        /// Loại tài sản trên 500 triệu
        /// </summary>
        public enum AssetOver500mType
        {
            /// <summary>
            /// Phương tiện vận tải khác
            /// </summary>
            OtherVehicle = 1,
            /// <summary>
            /// Máy móc, thiết bị văn phòng
            /// </summary>
            OfficeEquipment = 2,
            /// <summary>
            /// Thiết bị truyền dẫn
            /// </summary>
            TransmissionEquipment = 3,
            /// <summary>
            /// Máy móc, thiết bị động lực
            /// </summary>
            AutomotiveEquipment = 4,
            /// <summary>
            /// Máy móc, thiết bị chuyên dùng
            /// </summary>
            SpecializedEquipment = 5,
            /// <summary>
            /// Thiết bị đo lường, thí nghiệm
            /// </summary>
            LaboratoryEquipment = 6,
            /// <summary>
            /// Tài sản vô hình (không bao gồm quyền sử dụng đất)
            /// </summary>
            IntangibleAsset = 7,
            /// <summary>
            /// Tài sản khác
            /// </summary>
            Other
        }

        /// <summary>
        /// Loại tài sản kết cấu hạ tầng
        /// </summary>
        public enum InfrastructureAssetType
        {
            /// <summary>
            /// Công trình cấp nước sạch
            /// </summary>
            WaterSuppliy = 1,
            /// <summary>
            /// Tài sản KCHT thủy lợi
            /// </summary>
            Irrigation = 2,
            /// <summary>
            /// Tài sản KCHT giao thông đường thủy nội địa
            /// </summary>
            WaterwayTransportation = 3,
            /// <summary>
            /// Tài sản KCHT giao thông đường bộ
            /// </summary>
            RoadwayTransportation = 4,
            /// <summary>
            /// Tài sản KCHT đường sắt quốc gia
            /// </summary>
            RailwayTransportation = 5,
            /// <summary>
            /// Tài sản KCHT hàng không
            /// </summary>
            AirwayTransportation = 6,
            /// <summary>
            /// Tài sản KCHT hàng hải
            /// </summary>
            SeawayTransportation = 7,
            /// <summary>
            /// Tài sản KCHT khác
            /// </summary>
            Other = 8,
        }

        /// <summary>
        /// Cấp hành chính
        /// </summary>
        public enum AdministrativeLevel
        {
            /// <summary>
            /// Trung ương
            /// </summary>
            Central = 1,
            /// <summary>
            /// Địa phương
            /// </summary>
            Local = 2,
            /// <summary>
            /// Cấp tỉnh/thành phố
            /// </summary>
            Province = 3,
            /// <summary>
            /// Cấp quận/huyện
            /// </summary>
            District = 4,
            /// <summary>
            /// Cấp phường/xã
            /// </summary>
            Ward = 5
        }

        /// <summary>
        /// Loại hình đơn vị
        /// </summary>
        public enum UnitType
        {
            /// <summary>
            /// Cơ quan quản lý nhà nước
            /// </summary>
            StateManagement = 1,
            /// <summary>
            /// Đơn vị sự nghiệp
            /// </summary>
            BusinessUnit = 2,
            /// <summary>
            /// Các tổ chức chính phủ
            /// </summary>
            GovernmentInstitution = 3,
            /// <summary>
            /// Ban quản lý dự án
            /// </summary>
            ProjectManagement = 4
        }

        /// <summary>
        /// Nguồn gốc hình thành tài sản
        /// </summary>
        public enum AssetSource
        {
            /// <summary>
            /// Đầu tư xây dựng/mua sắm
            /// </summary>
            Investment = 1,
            /// <summary>
            /// Tài sản được giao mới
            /// </summary>
            Reception = 2,
            /// <summary>
            /// Tài sản đi thuê
            /// </summary>
            Lease = 3,
            /// <summary>
            /// Tài sản từ nguồn viện trợ, quà biếu, quà tặng
            /// </summary>
            Donation = 4
        }

        /// <summary>
        /// Phương thức khai thác
        /// </summary>
        public enum ExploitationType
        {
            /// <summary>
            /// Phương thức khai thác trực tiếp
            /// </summary>
            Direct = 1,
            /// <summary>
            /// Cho thuê quyền khai thác
            /// </summary>
            Lease = 2,
            /// <summary>
            /// Chuyển nhượng có thời hạn quyền khai thác
            /// </summary>
            TransferWithLimitedTime = 3,
            /// <summary>
            /// Chuyển nhượng thu phí
            /// </summary>
            TransferWithCharge = 4,
            /// <summary>
            /// Phương thức khai thác khác
            /// </summary>
            Other = 5
        }

        /// <summary>
        /// Hình thức xử lý
        /// </summary>
        public enum HandlingType
        {
            /// <summary>
            /// Bán
            /// </summary>
            ForSale = 1,
            /// <summary>
            /// Thanh lý
            /// </summary>
            Liquidation = 2,
            /// <summary>
            /// Điều chuyển
            /// </summary>
            Transfer = 3,
            /// <summary>
            /// Thu hồi
            /// </summary>
            Retrieve = 4,
            /// <summary>
            /// Sử dụng tài sản để thanh toán cho nhà đầu tư theo hình thức BT
            /// </summary>
            BuildTransfer = 5,
            /// <summary>
            /// Bị mất/bị hủy hoại
            /// </summary>
            LostOrRuined = 6,
            /// <summary>
            /// Hình thức xử lý khác
            /// </summary>
            Other = 7
        }

        /// <summary>
        /// Mục đích sử dụng đất
        /// </summary>
        public enum LandUsagePurpose
        {
            /// <summary>
            /// Đất trụ sở
            /// </summary>
            Headquarter = 1,
            /// <summary>
            /// Đất phục vụ hoạt động giáo dục và đào tạo
            /// </summary>
            EducationService = 2,
            /// <summary>
            /// Đất phục vụ hoạt động y tế
            /// </summary>
            HealthService = 3,
            /// <summary>
            /// Đất phục vụ hoạt động văn hóa
            /// </summary>
            CulturalActivity = 4,
            /// <summary>
            /// Đất phục vụ hoạt động thể dục thể thao
            /// </summary>
            SportActivity = 5,
            /// <summary>
            /// Đất phục vụ hoạt động sự nghiệp nông nghiệp
            /// </summary>
            AgriculturalBusiness = 6,
            /// <summary>
            /// Đất phục vụ hoạt động thông tin, truyền thông
            /// </summary>
            MediaService = 7,
            /// <summary>
            /// Đất phục vụ hoạt động sự nghiệp khoa học, công nghệ
            /// </summary>
            TechnologyActivity = 8,
            /// <summary>
            /// Đất công trình công cộng
            /// </summary>
            PublicUse = 9,
            /// <summary>
            /// Đất phục vụ hoạt động sự nghiệp nông nghiệp, phục vụ công tác chuyên môn, hoạt động sự nghiệp khác
            /// </summary>
            Other = 10
        }

        /// <summary>
        /// Mục đích sử dụng xe
        /// </summary>
        public enum VehicleUsagePurpose
        {
            /// <summary>
            /// Xe ô tô phục vụ chức danh
            /// </summary>
            Individual = 1,
            /// <summary>
            /// Xe phục vụ công tác chung
            /// </summary>
            Common = 2,
            /// <summary>
            /// Xe chuyên dùng
            /// </summary>
            Specialized = 3
        }

        /// <summary>
        /// Loại xe
        /// </summary>
        public enum VehicleType
        {
            /// <summary>
            /// Loại 4-5 chỗ
            /// </summary>
            Seat4To5 = 1,
            /// <summary>
            /// Loại 6-8 chỗ
            /// </summary>
            Seat6To8 = 2,
            /// <summary>
            /// Loại 9-12 chỗ
            /// </summary>
            Seat9To12 = 3,
            /// <summary>
            /// Loại 13-16 chỗ
            /// </summary>
            Seat13To16 = 4,
            /// <summary>
            /// Xe cứu thương
            /// </summary>
            Ambulance = 5,
            /// <summary>
            /// Xe tập lái
            /// </summary>
            Practice = 6,
            /// <summary>
            /// Xe tải
            /// </summary>
            Truck = 7,
            /// <summary>
            /// Loại xe chuyên dùng khác
            /// </summary>
            OtherSpecialized = 8
        }

    }
}
