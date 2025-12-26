namespace RealEstateAgency.Models
{
    using System;
    using System.Collections.Generic;

    public class EstateFile
    {
        public EstateFile()
        {
            FileGroupRelations = new List<FileGroupRelation>();
            FileRefers = new List<FileRefer>();
            FileRequests = new List<FileRequest>();
        }
        public long FileID { get; set; }
        public long PersonID { get; set; }
        public string FileCode { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Area { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal RoomCount { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public string FileType { get; set; }
        public string EstateType { get; set; }
        public string FileDescription { get; set; }
        public int ProvinceID { get; set; }
        public int? CityID { get; set; }
        public int? ZoneID { get; set; }
        public string AddressStreet { get; set; }
        public string AddressAlley { get; set; }
        public string AddressNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateByID { get; set; }
        public int? Consultant { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByID { get; set; }
        public string BuildingFacade { get; set; }
        public string Heating { get; set; }
        public string Cooling { get; set; }
        public string EstatePhoneNumber { get; set; }
        public string Status { get; set; }
        public string OwnershipDocumentStatus { get; set; }
        public decimal BuildingFloorCount { get; set; }
        public decimal FloorNumber { get; set; }
        public decimal BuiltUpArea { get; set; }
        public string Kitchen { get; set; }
        public string RestRoom { get; set; }
        public string InformationSource { get; set; }
        public string AdvContent1 { get; set; }
        public string AdvContent2 { get; set; }
        public string AdvContent3 { get; set; }
        public decimal ParkingCount { get; set; }
        public decimal BuildingAge { get; set; }
        public decimal MasterRoomCount { get; set; }
        public decimal StoreRoomArea { get; set; }
        public string Flooring { get; set; }
        public decimal RestRoomCount { get; set; }
        public decimal UnitCountInFloor { get; set; }
        public decimal ElevatorCount { get; set; }
        public decimal TerraceArea { get; set; }
        public string FacadeMaterial { get; set; }
        public decimal LobbyCeilingHeight { get; set; }
        public decimal UnitCeilingHeight { get; set; }
        public string ElevatorBrand { get; set; }
        public string ValvesBrand { get; set; }
        public string BlockPieceNumber { get; set; }
        public decimal PassWidth { get; set; }
        public decimal YardAread { get; set; }
        public decimal BasementArea { get; set; }
        public decimal FirstFloorArea { get; set; }
        public decimal SecondFloorArea { get; set; }
        public decimal ThirdFloorArea { get; set; }
        public decimal GroundFloorArea { get; set; }
        public string FloorsArchitecture { get; set; }
        public decimal DetailedPlanArea { get; set; }
        public decimal KitchenCount { get; set; }
        public string OneWayTwoWayStreet { get; set; }
        public string JobTenure { get; set; }
        public decimal BalconyArea { get; set; }
        public decimal AreaForBoard { get; set; }
        public decimal AllUnitsCount { get; set; }
        public decimal AreaForCommerce { get; set; }
        public decimal PhoneLineCount { get; set; }
        public bool HasStoreRoom { get; set; }
        public bool HasElevator { get; set; }
        public bool HasParking { get; set; }
        public bool HasRemoteDoor { get; set; }
        public bool HasServantRestroom { get; set; }
        public bool HasBasement { get; set; }
        public bool HasBackyard { get; set; }
        public bool HasYard { get; set; }
        public bool HasVideoDoorPhone { get; set; }
        public bool NeedToRebuild { get; set; }
        public bool IntegratedSalon { get; set; }
        public bool SeparateSuite { get; set; }
        public bool HasLaundryRoom { get; set; }
        public bool ResidentOwner { get; set; }
        public bool IsRealEstate { get; set; }
        public bool HasPool { get; set; }
        public bool HasSauna { get; set; }
        public bool HasJacuzzi { get; set; }
        public bool HasRoofGarden { get; set; }
        public bool HasConferenceHall { get; set; }
        public bool HasLobby { get; set; }
        public bool HasGym { get; set; }
        public bool HasCinema { get; set; }
        public bool HasBilliard { get; set; }
        public bool HasSquash { get; set; }
        public bool HasRoofPool { get; set; }
        public bool HasFurnishedLobby { get; set; }
        public bool HasEntrance { get; set; }
        public bool HasLobbyMan { get; set; }
        public bool HasMaidRoom { get; set; }
        public bool HasSeparateSitting { get; set; }
        public bool HasInteriorPool { get; set; }
        public bool HasCookingKitchen { get; set; }
        public bool HasFurnishKitchen { get; set; }
        public bool HasSemiFurnishKitchen { get; set; }
        public bool HasGardenPermission { get; set; }
        public bool HasRooflessPool { get; set; }
        public bool HasBuildingLicense { get; set; }
        public bool HasPlanCommand { get; set; }
        public bool HasWatchman { get; set; }
        public bool InMall { get; set; }
        public bool HasPatio { get; set; }
        public bool HasSeparateStaircase { get; set; }
        public bool HasBigTerrace { get; set; }
        public bool HasPanoramaView { get; set; }
        public bool HasSouthView { get; set; }
        public bool HasNorthView { get; set; }
        public bool HasWoodyView { get; set; }
        public bool HasResidentialStatus { get; set; }
        public bool HasGardenDocument { get; set; }
        public bool BoardMountable { get; set; }
        public bool HasOfficeDocument { get; set; }
        public bool HasGoodWill { get; set; }
        public bool HasOwnership { get; set; }
        public bool IsFurnitured { get; set; }
        public bool ToOutsider { get; set; }
        public decimal Deposit { get; set; }
        public decimal Rent { get; set; }
        public virtual City City { get; set; }
        public virtual Province Province { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<FileGroupRelation> FileGroupRelations { get; set; }
        public virtual User UpdateBy { get; set; }
        public virtual User CreateBy { get; set; }
        public virtual User ConsultantUser { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<FileRefer> FileRefers { get; set; }
        public virtual ICollection<FileRequest> FileRequests { get; set; }

    }
}
