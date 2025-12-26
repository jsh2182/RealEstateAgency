using RealEstateAgency.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.EFDataService
{
    public class EFEstateFileDataService : EFBaseDataService<EstateFile, long>, DataService.IEstateFileDataService
    {
        public EFEstateFileDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<EstateFile, long>> GetKey()
        {
            return e => e.FileID;
        }
        public IQueryable<EstateFileSchema> Search(EstateFileSchema model)
        {
            var currentUser = Tools.CurrentUser;
            var qry = All();
            if (!string.IsNullOrEmpty(model.PersonName))
                qry = qry.Where(q => q.Person.PersonName.Contains(model.PersonName));
            if (!string.IsNullOrEmpty(model.AddressStreet))
                qry = qry.Where(q => q.AddressStreet.Contains(model.AddressStreet));
            if (!string.IsNullOrEmpty(model.AddressAlley))
                qry = qry.Where(q => q.AddressAlley.Contains(model.AddressAlley));
            if (!string.IsNullOrEmpty(model.AddressNumber))
                qry = qry.Where(q => q.AddressNumber.Equals(model.AddressNumber));
            if (model.Area.HasValue)
                qry = qry.Where(q => q.Area >= model.Area);
            if (model.AreaTo.HasValue)
                qry = qry.Where(q => q.Area <= model.AreaTo);
            if (model.CityID.HasValue)
                qry = qry.Where(q => q.CityID == model.CityID);
            if (model.ForRequest && !currentUser.IsAdmin)
                qry = qry.Where(q => q.CreateByID != currentUser.UserID);
            else if (!model.ForRequest && !currentUser.IsAdmin)
                qry = qry.Where(q => q.CreateByID == currentUser.UserID);
            if (model.CreateDate > DateTime.MinValue)
                qry = qry.Where(q => q.CreateDate >= model.CreateDate);
            if (model.CreateDateTo.HasValue)
                qry = qry.Where(q => q.CreateDate <= model.CreateDate);
            if (!string.IsNullOrEmpty(model.EstateType))
                qry = qry.Where(q => q.EstateType == model.EstateType);
            if (!string.IsNullOrEmpty(model.FileCode))
                qry = qry.Where(q => q.FileCode.Contains(model.FileCode));
            if (!string.IsNullOrEmpty(model.FileDescription))
                qry = qry.Where(q => q.FileDescription.Contains(model.FileDescription));
            if (model.FileID > 0)
                qry = qry.Where(q => q.FileID == model.FileID);
            if (model.GroupID.HasValue)
                qry = qry.Where(q => q.FileGroupRelations.Any(g => g.GroupID == model.GroupID));
            if (model.Latitude.HasValue)
                qry = qry.Where(q => q.Latitude == model.Latitude);
            if (model.Longtitude.HasValue)
                qry = qry.Where((q => q.Longtitude == model.Longtitude));
            if (model.ProvinceID > 0)
                qry = qry.Where(q => q.ProvinceID == model.ProvinceID);
            if (model.RoomCount > 0)
                qry = qry.Where(q => q.RoomCount == model.RoomCount);
            if (model.TotalPrice > 0)
                qry = qry.Where(q => q.TotalPrice >= model.TotalPrice);
            if (model.TotalPriceTo > 0)
                qry = qry.Where(q => q.TotalPrice <= model.TotalPriceTo);
            if (model.UnitPrice > 0)
                qry = qry.Where(q => q.UnitPrice <= model.UnitPrice);
            if (model.UnitPriceTo.HasValue)
                qry = qry.Where(q => q.UnitPrice <= model.UnitPriceTo);
            if (model.UpdateByID.HasValue)
                qry = qry.Where(q => q.UpdateByID == model.UpdateByID);
            if (model.UpdateDate.HasValue)
                qry = qry.Where(q => q.UpdateDate >= model.UpdateDate);
            if (model.UpdateDateTo.HasValue)
                qry = qry.Where(q => q.UpdateDate <= model.UpdateDateTo);
            if (model.ZoneID.HasValue)
                qry = qry.Where(q => q.ZoneID == model.ZoneID);
            if (!string.IsNullOrEmpty(model.BuildingFacade))
                qry = qry.Where(q => q.BuildingFacade == model.BuildingFacade);
            if (model.BuildingFloorCount > 0)
                qry = qry.Where(q => q.BuildingFloorCount == model.BuildingFloorCount);
            if (model.BuiltUpArea > 0)
                qry = qry.Where(q => q.BuiltUpArea == model.BuiltUpArea);
            if (!string.IsNullOrEmpty(model.Cooling))
                qry = qry.Where(q => q.Cooling == model.Cooling);
            if (!string.IsNullOrEmpty(model.EstatePhoneNumber))
                qry = qry.Where(q => q.EstatePhoneNumber.Contains(model.EstatePhoneNumber));
            if (model.FloorNumber > 0)
                qry = qry.Where(q => q.FloorNumber == model.FloorNumber);
            if (model.HasBackyard.HasValue)
                qry = qry.Where(q => q.HasBackyard == model.HasBackyard);
            if (model.HasBasement.HasValue)
                qry = qry.Where(q => q.HasBasement == model.HasBasement);
            if (model.HasElevator.HasValue)
                qry = qry.Where(q => q.HasElevator == model.HasElevator);
            if (model.HasParking.HasValue)
                qry = qry.Where(q => q.HasParking == model.HasParking);
            if (model.HasRemoteDoor.HasValue)
                qry = qry.Where(q => q.HasParking == model.HasParking);
            if (model.HasServantRestroom.HasValue)
                qry = qry.Where(q => q.HasServantRestroom == model.HasServantRestroom);
            if (model.HasStoreRoom.HasValue)
                qry = qry.Where(q => q.HasStoreRoom == model.HasStoreRoom);
            if (model.HasVideoDoorPhone.HasValue)
                qry = qry.Where(q => q.HasVideoDoorPhone == model.HasVideoDoorPhone);
            if (model.HasYard.HasValue)
                qry = qry.Where(q => q.HasYard == model.HasYard);
            if (!string.IsNullOrEmpty(model.Heating))
                qry = qry.Where(q => q.Heating == model.Heating);
            if (!string.IsNullOrEmpty(model.InformationSource))
                qry = qry.Where(q => q.InformationSource == model.InformationSource);
            if (!string.IsNullOrEmpty(model.Kitchen))
                qry = qry.Where(q => q.Kitchen == model.Kitchen);
            if (!string.IsNullOrEmpty(model.OwnershipDocumentStatus))
                qry = qry.Where(q => q.OwnershipDocumentStatus == model.OwnershipDocumentStatus);
            if (!string.IsNullOrEmpty(model.RestRoom))
                qry = qry.Where(q => q.RestRoom == model.RestRoom);
            if (!string.IsNullOrEmpty(model.Status))
                qry = qry.Where(q => q.Status == model.Status);
            if (model.ParkingCount > 0)
                qry = qry.Where(q => q.ParkingCount == model.ParkingCount);
            if (model.BuildingAge > 0)
                qry = qry.Where(q => q.BuildingAge == model.BuildingAge);
            if (model.MasterRoomCount > 0)
                qry = qry.Where(q => q.MasterRoomCount == model.MasterRoomCount);
            if (model.StoreRoomArea > 0)
                qry = qry.Where(q => q.StoreRoomArea == model.StoreRoomArea);
            if (model.RestRoomCount > 0)
                qry = qry.Where(q => q.RestRoomCount == model.RestRoomCount);
            if (model.UnitCountInFloor > 0)
                qry = qry.Where(q => q.UnitCountInFloor == model.UnitCountInFloor);
            if (model.ElevatorCount > 0)
                qry = qry.Where(q => q.ElevatorCount == model.ElevatorCount);
            if (model.TerraceArea > 0)
                qry = qry.Where(q => q.TerraceArea == model.TerraceArea);
            if (model.LobbyCeilingHeight > 0)
                qry = qry.Where(q => q.LobbyCeilingHeight == model.LobbyCeilingHeight);
            if (model.UnitCeilingHeight > 0)
                qry = qry.Where(q => q.UnitCeilingHeight == model.UnitCeilingHeight);
            if (model.PassWidth > 0)
                qry = qry.Where(q => q.PassWidth == model.PassWidth);
            if (model.YardAread > 0)
                qry = qry.Where(q => q.YardAread == model.YardAread);
            if (model.BasementArea > 0)
                qry = qry.Where(q => q.BasementArea == model.BasementArea);
            if (model.FirstFloorArea > 0)
                qry = qry.Where(q => q.FirstFloorArea == model.FirstFloorArea);
            if (model.SecondFloorArea > 0)
                qry = qry.Where(q => q.SecondFloorArea == model.SecondFloorArea);
            if (model.ThirdFloorArea > 0)
                qry = qry.Where(q => q.ThirdFloorArea == model.ThirdFloorArea);
            if (model.GroundFloorArea > 0)
                qry = qry.Where(q => q.GroundFloorArea == model.GroundFloorArea);
            if (model.DetailedPlanArea > 0)
                qry = qry.Where(q => q.DetailedPlanArea == model.DetailedPlanArea);
            if (model.KitchenCount > 0)
                qry = qry.Where(q => q.KitchenCount == model.KitchenCount);
            if (model.BalconyArea > 0)
                qry = qry.Where(q => q.BalconyArea == model.BalconyArea);
            if (model.AreaForBoard > 0)
                qry = qry.Where(q => q.AreaForBoard == model.AreaForBoard);
            if (model.AllUnitsCount > 0)
                qry = qry.Where(q => q.AllUnitsCount == model.AllUnitsCount);
            if (model.AreaForCommerce > 0)
                qry = qry.Where(q => q.AreaForCommerce == model.AreaForCommerce);
            if (model.PhoneLineCount > 0)
                qry = qry.Where(q => q.PhoneLineCount == model.PhoneLineCount);
            if (!string.IsNullOrEmpty(model.Flooring))
                qry = qry.Where(q => q.Flooring == model.Flooring);
            if (!string.IsNullOrEmpty(model.FacadeMaterial))
                qry = qry.Where(q => q.FacadeMaterial == model.FacadeMaterial);
            if (!string.IsNullOrEmpty(model.ElevatorBrand))
                qry = qry.Where(q => q.ElevatorBrand == model.ElevatorBrand);
            if (!string.IsNullOrEmpty(model.ValvesBrand))
                qry = qry.Where(q => q.ValvesBrand == model.ValvesBrand);
            if (!string.IsNullOrEmpty(model.BlockPieceNumber))
                qry = qry.Where(q => q.BlockPieceNumber == model.BlockPieceNumber);
            if (!string.IsNullOrEmpty(model.FloorsArchitecture))
                qry = qry.Where(q => q.FloorsArchitecture == model.FloorsArchitecture);
            if (!string.IsNullOrEmpty(model.OneWayTwoWayStreet))
                qry = qry.Where(q => q.OneWayTwoWayStreet == model.OneWayTwoWayStreet);
            if (!string.IsNullOrEmpty(model.JobTenure))
                qry = qry.Where(q => q.JobTenure == model.JobTenure);
            if (model.HasSauna.HasValue)
                qry = qry.Where(q => q.HasSauna == model.HasSauna);
            if (model.HasJacuzzi.HasValue)
                qry = qry.Where(q => q.HasJacuzzi == model.HasJacuzzi);
            if (model.HasRoofGarden.HasValue)
                qry = qry.Where(q => q.HasRoofGarden == model.HasRoofGarden);
            if (model.HasConferenceHall.HasValue)
                qry = qry.Where(q => q.HasConferenceHall == model.HasConferenceHall);
            if (model.HasLobby.HasValue)
                qry = qry.Where(q => q.HasLobby == model.HasLobby);
            if (model.HasGym.HasValue)
                qry = qry.Where(q => q.HasGym == model.HasGym);
            if (model.HasCinema.HasValue)
                qry = qry.Where(q => q.HasCinema == model.HasCinema);
            if (model.HasBilliard.HasValue)
                qry = qry.Where(q => q.HasBilliard == model.HasBilliard);
            if (model.HasSquash.HasValue)
                qry = qry.Where(q => q.HasSquash == model.HasSquash);
            if (model.HasRoofPool.HasValue)
                qry = qry.Where(q => q.HasRoofPool == model.HasRoofPool);
            if (model.HasFurnishedLobby.HasValue)
                qry = qry.Where(q => q.HasFurnishedLobby == model.HasFurnishedLobby);
            if (model.HasEntrance.HasValue)
                qry = qry.Where(q => q.HasEntrance == model.HasEntrance);
            if (model.HasLobbyMan.HasValue)
                qry = qry.Where(q => q.HasLobbyMan == model.HasLobbyMan);
            if (model.HasMaidRoom.HasValue)
                qry = qry.Where(q => q.HasMaidRoom == model.HasMaidRoom);
            if (model.HasSeparateSitting.HasValue)
                qry = qry.Where(q => q.HasSeparateSitting == model.HasSeparateSitting);
            if (model.HasInteriorPool.HasValue)
                qry = qry.Where(q => q.HasInteriorPool == model.HasInteriorPool);
            if (model.HasCookingKitchen.HasValue)
                qry = qry.Where(q => q.HasCookingKitchen == model.HasCookingKitchen);
            if (model.HasFurnishKitchen.HasValue)
                qry = qry.Where(q => q.HasFurnishKitchen == model.HasFurnishKitchen);
            if (model.HasSemiFurnishKitchen.HasValue)
                qry = qry.Where(q => q.HasSemiFurnishKitchen == model.HasSemiFurnishKitchen);
            if (model.HasGardenPermission.HasValue)
                qry = qry.Where(q => q.HasGardenPermission == model.HasGardenPermission);
            if (model.HasRooflessPool.HasValue)
                qry = qry.Where(q => q.HasRooflessPool == model.HasRooflessPool);
            if (model.HasBuildingLicense.HasValue)
                qry = qry.Where(q => q.HasBuildingLicense == model.HasBuildingLicense);
            if (model.HasPlanCommand.HasValue)
                qry = qry.Where(q => q.HasPlanCommand == model.HasPlanCommand);
            if (model.HasWatchman.HasValue)
                qry = qry.Where(q => q.HasWatchman == model.HasWatchman);
            if (model.HasPatio.HasValue)
                qry = qry.Where(q => q.HasPatio == model.HasPatio);
            if (model.HasSeparateStaircase.HasValue)
                qry = qry.Where(q => q.HasSeparateStaircase == model.HasSeparateStaircase);
            if (model.HasBigTerrace.HasValue)
                qry = qry.Where(q => q.HasBigTerrace == model.HasBigTerrace);
            if (model.HasPanoramaView.HasValue)
                qry = qry.Where(q => q.HasPanoramaView == model.HasPanoramaView);
            if (model.HasSouthView.HasValue)
                qry = qry.Where(q => q.HasSouthView == model.HasSouthView);
            if (model.HasNorthView.HasValue)
                qry = qry.Where(q => q.HasNorthView == model.HasNorthView);
            if (model.HasWoodyView.HasValue)
                qry = qry.Where(q => q.HasWoodyView == model.HasWoodyView);
            if (model.HasResidentialStatus.HasValue)
                qry = qry.Where(q => q.HasResidentialStatus == model.HasResidentialStatus);
            if (model.HasGardenDocument.HasValue)
                qry = qry.Where(q => q.HasGardenDocument == model.HasGardenDocument);
            if (model.BoardMountable.HasValue)
                qry = qry.Where(q => q.BoardMountable == model.BoardMountable);
            if (model.HasOfficeDocument.HasValue)
                qry = qry.Where(q => q.HasOfficeDocument == model.HasOfficeDocument);
            if (model.HasGoodWill.HasValue)
                qry = qry.Where(q => q.HasGoodWill == model.HasGoodWill);
            if (model.HasOwnership.HasValue)
                qry = qry.Where(q => q.HasOwnership == model.HasOwnership);
            if (model.IsFurnitured.HasValue)
                qry = qry.Where(q => q.IsFurnitured == model.IsFurnitured);
            if (model.ToOutsider.HasValue)
                qry = qry.Where(q => q.ToOutsider == model.ToOutsider);
            if (model.Rent > 0)
                qry = qry.Where(q => q.Rent == model.Rent);
            if (model.Deposit > 0)
                qry = qry.Where(q => q.Deposit == model.Deposit);

            if (model.HasPool.HasValue)
                qry = qry.Where(q => q.HasPool == model.HasPool);
            if (model.IsRealEstate.HasValue)
                qry = qry.Where(q => q.IsRealEstate == model.IsRealEstate);
            if (model.ResidentOwner.HasValue)
                qry = qry.Where(q => q.ResidentOwner == model.ResidentOwner);
            if (model.HasLaundryRoom.HasValue)
                qry = qry.Where(q => q.HasLaundryRoom == model.HasLaundryRoom);
            if (model.SeparateSuite.HasValue)
                qry = qry.Where(q => q.SeparateSuite == model.SeparateSuite);
            if (model.IntegratedSalon.HasValue)
                qry = qry.Where(q => q.IntegratedSalon == model.IntegratedSalon);
            if (model.NeedToRebuild.HasValue)
                qry = qry.Where(q => q.NeedToRebuild == model.NeedToRebuild);
            var result = qry.Select(q => new EstateFileSchema()
            {
                AddressStreet = q.AddressStreet,
                AddressAlley = q.AddressAlley,
                AddressNumber = q.AddressNumber,
                Area = q.Area,
                CityID = q.CityID,
                CityName = q.City.CityName,
                CreateByID = q.CreateByID,
                CreateDate = q.CreateDate,
                EstateType = q.EstateType,
                FileCode = q.FileCode,
                FileDescription = q.FileDescription,
                FileID = q.FileID,
                FileType = q.FileType,
                Latitude = q.Latitude,
                Length = q.Length,
                Longtitude = q.Longtitude,
                PersonID = q.PersonID,
                PersonName = q.Person.PersonName,
                ProvinceID = q.ProvinceID,
                ProvinceName = q.Province.ProvinceName,
                RoomCount = q.RoomCount,
                TotalPrice = q.TotalPrice,
                UnitPrice = q.UnitPrice,
                UpdateByID = q.UpdateByID,
                UpdateDate = q.UpdateDate,
                Width = q.Width,
                ZoneID = q.ZoneID,
                BuildingFacade = q.BuildingFacade,
                BuildingFloorCount = q.BuildingFloorCount,
                BuiltUpArea = q.BuiltUpArea,
                Cooling = q.Cooling,
                EstatePhoneNumber = q.EstatePhoneNumber,
                FloorNumber = q.FloorNumber,
                HasBackyard = q.HasBackyard,
                HasBasement = q.HasBasement,
                HasElevator = q.HasElevator,
                HasParking = q.HasParking,
                HasRemoteDoor = q.HasRemoteDoor,
                HasServantRestroom = q.HasServantRestroom,
                HasStoreRoom = q.HasStoreRoom,
                HasVideoDoorPhone = q.HasVideoDoorPhone,
                HasYard = q.HasYard,
                Heating = q.Heating,
                InformationSource = q.InformationSource,
                Kitchen = q.Kitchen,
                OwnershipDocumentStatus = q.OwnershipDocumentStatus,
                RestRoom = q.RestRoom,
                Status = q.Status

            });
            return result;

        }
        private EstateFile InitEstateFile(EstateFileSchema model)
        {
            EstateFile estateFile;
            if (model.FileID > 0)
            {
                estateFile = Get(model.FileID);
                estateFile.UpdateByID = Tools.CurrentUser.UserID;
                estateFile.UpdateDate = DateTime.Now;

            }
            else
            {
                estateFile = new EstateFile()
                {
                    CreateDate = DateTime.Now,
                    CreateByID = Tools.CurrentUser.UserID,
                    IsDeleted = false
                };
            }
            estateFile.AddressStreet = model.AddressStreet;
            estateFile.AddressAlley = model.AddressAlley;
            estateFile.AddressNumber = model.AddressNumber;
            estateFile.Area = model.Area;
            estateFile.CityID = model.CityID;
            estateFile.EstateType = model.EstateType;
            estateFile.FileCode = model.FileCode;
            estateFile.FileDescription = model.FileDescription;
            estateFile.FileType = model.FileType;
            estateFile.Latitude = model.Latitude;
            estateFile.Longtitude = model.Longtitude;
            estateFile.PersonID = model.PersonID;
            estateFile.ProvinceID = model.ProvinceID;
            estateFile.RoomCount = model.RoomCount;
            estateFile.TotalPrice = model.TotalPrice;
            estateFile.UnitPrice = model.UnitPrice;
            estateFile.Width = model.Width;
            estateFile.ZoneID = model.ZoneID;
            estateFile.BuildingFacade = model.BuildingFacade;
            estateFile.BuildingFloorCount = model.BuildingFloorCount;
            estateFile.BuiltUpArea = model.BuiltUpArea;
            estateFile.Cooling = model.Cooling;
            estateFile.EstatePhoneNumber = model.EstatePhoneNumber;
            estateFile.FloorNumber = model.FloorNumber;
            estateFile.Heating = model.Heating;
            estateFile.InformationSource = model.InformationSource;
            estateFile.Kitchen = model.Kitchen;
            estateFile.OwnershipDocumentStatus = model.OwnershipDocumentStatus;
            estateFile.RestRoom = model.RestRoom;
            estateFile.Status = model.Status;
            if (model.HasBackyard.HasValue)
                estateFile.HasBackyard = (bool)model.HasBackyard;
            if (model.HasBasement.HasValue)
                estateFile.HasBasement = (bool)model.HasBasement;
            if (model.HasElevator.HasValue)
                estateFile.HasElevator = (bool)model.HasElevator;
            if (model.HasParking.HasValue)
                estateFile.HasParking = (bool)model.HasParking;
            if (model.HasRemoteDoor.HasValue)
                estateFile.HasRemoteDoor = (bool)model.HasRemoteDoor;
            if (model.HasServantRestroom.HasValue)
                estateFile.HasServantRestroom = (bool)model.HasServantRestroom;
            if (model.HasStoreRoom.HasValue)
                estateFile.HasStoreRoom = (bool)model.HasStoreRoom;
            if (model.HasVideoDoorPhone.HasValue)
                estateFile.HasVideoDoorPhone = (bool)model.HasVideoDoorPhone;
            if (model.HasYard.HasValue)
                estateFile.HasYard = (bool)model.HasYard;
            estateFile.ParkingCount = model.ParkingCount;
            estateFile.BuildingAge = model.BuildingAge;
            estateFile.MasterRoomCount = model.MasterRoomCount;
            estateFile.StoreRoomArea = model.StoreRoomArea;
            estateFile.RestRoomCount = model.RestRoomCount;
            estateFile.UnitCountInFloor = model.UnitCountInFloor;
            estateFile.ElevatorCount = model.ElevatorCount;
            estateFile.TerraceArea = model.TerraceArea;
            estateFile.LobbyCeilingHeight = model.LobbyCeilingHeight;
            estateFile.UnitCeilingHeight = model.UnitCeilingHeight;
            estateFile.PassWidth = model.PassWidth;
            estateFile.YardAread = model.YardAread;
            estateFile.BasementArea = model.BasementArea;
            estateFile.FirstFloorArea = model.FirstFloorArea;
            estateFile.SecondFloorArea = model.SecondFloorArea;
            estateFile.ThirdFloorArea = model.ThirdFloorArea;
            estateFile.GroundFloorArea = model.GroundFloorArea;
            estateFile.DetailedPlanArea = model.DetailedPlanArea;
            estateFile.KitchenCount = model.KitchenCount;
            estateFile.BalconyArea = model.BalconyArea;
            estateFile.AreaForBoard = model.AreaForBoard;
            estateFile.AllUnitsCount = model.AllUnitsCount;
            estateFile.AreaForCommerce = model.AreaForCommerce;
            estateFile.PhoneLineCount = model.PhoneLineCount;
            estateFile.Flooring = model.Flooring;
            estateFile.FacadeMaterial = model.FacadeMaterial;
            estateFile.ElevatorBrand = model.ElevatorBrand;
            estateFile.ValvesBrand = model.ValvesBrand;
            estateFile.BlockPieceNumber = model.BlockPieceNumber;
            estateFile.FloorsArchitecture = model.FloorsArchitecture;
            estateFile.OneWayTwoWayStreet = model.OneWayTwoWayStreet;
            estateFile.JobTenure = model.JobTenure;
            estateFile.AdvContent1 = model.AdvContent1;
            estateFile.AdvContent2 = model.AdvContent2;
            estateFile.AdvContent3 = model.AdvContent3;
            estateFile.Deposit = model.Deposit;
            estateFile.Rent = model.Rent;
            if (model.HasSauna.HasValue)
                estateFile.HasSauna = (bool)model.HasSauna;
            if (model.HasJacuzzi.HasValue)
                estateFile.HasJacuzzi = (bool)model.HasJacuzzi;
            if (model.HasRoofGarden.HasValue)
                estateFile.HasRoofGarden = (bool)model.HasRoofGarden;
            if (model.HasConferenceHall.HasValue)
                estateFile.HasConferenceHall = (bool)model.HasConferenceHall;
            if (model.HasLobby.HasValue)
                estateFile.HasLobby = (bool)model.HasLobby;
            if (model.HasGym.HasValue)
                estateFile.HasGym = (bool)model.HasGym;
            if (model.HasCinema.HasValue)
                estateFile.HasCinema = (bool)model.HasCinema;
            if (model.HasBilliard.HasValue)
                estateFile.HasBilliard = (bool)model.HasBilliard;
            if (model.HasSquash.HasValue)
                estateFile.HasSquash = (bool)model.HasSquash;
            if (model.HasRoofPool.HasValue)
                estateFile.HasRoofPool = (bool)model.HasRoofPool;
            if (model.HasFurnishedLobby.HasValue)
                estateFile.HasFurnishedLobby = (bool)model.HasFurnishedLobby;
            if (model.HasEntrance.HasValue)
                estateFile.HasEntrance = (bool)model.HasEntrance;
            if (model.HasLobbyMan.HasValue)
                estateFile.HasLobbyMan = (bool)model.HasLobbyMan;
            if (model.HasMaidRoom.HasValue)
                estateFile.HasMaidRoom = (bool)model.HasMaidRoom;
            if (model.HasSeparateSitting.HasValue)
                estateFile.HasSeparateSitting = (bool)model.HasSeparateSitting;
            if (model.HasInteriorPool.HasValue)
                estateFile.HasInteriorPool = (bool)model.HasInteriorPool;
            if (model.HasCookingKitchen.HasValue)
                estateFile.HasCookingKitchen = (bool)model.HasCookingKitchen;
            if (model.HasFurnishKitchen.HasValue)
                estateFile.HasFurnishKitchen = (bool)model.HasFurnishKitchen;
            if (model.HasSemiFurnishKitchen.HasValue)
                estateFile.HasSemiFurnishKitchen = (bool)model.HasSemiFurnishKitchen;
            if (model.HasGardenPermission.HasValue)
                estateFile.HasGardenPermission = (bool)model.HasGardenPermission;
            if (model.HasRooflessPool.HasValue)
                estateFile.HasRooflessPool = (bool)model.HasRooflessPool;
            if (model.HasBuildingLicense.HasValue)
                estateFile.HasBuildingLicense = (bool)model.HasBuildingLicense;
            if (model.HasPlanCommand.HasValue)
                estateFile.HasPlanCommand = (bool)model.HasPlanCommand;
            if (model.HasWatchman.HasValue)
                estateFile.HasWatchman = (bool)model.HasWatchman;
            if (model.HasPatio.HasValue)
                estateFile.HasPatio = (bool)model.HasPatio;
            if (model.HasSeparateStaircase.HasValue)
                estateFile.HasSeparateStaircase = (bool)model.HasSeparateStaircase;
            if (model.HasBigTerrace.HasValue)
                estateFile.HasBigTerrace = (bool)model.HasBigTerrace;
            if (model.HasPanoramaView.HasValue)
                estateFile.HasPanoramaView = (bool)model.HasPanoramaView;
            if (model.HasSouthView.HasValue)
                estateFile.HasSouthView = (bool)model.HasSouthView;
            if (model.HasNorthView.HasValue)
                estateFile.HasNorthView = (bool)model.HasNorthView;
            if (model.HasWoodyView.HasValue)
                estateFile.HasWoodyView = (bool)model.HasWoodyView;
            if (model.HasResidentialStatus.HasValue)
                estateFile.HasResidentialStatus = (bool)model.HasResidentialStatus;
            if (model.HasGardenDocument.HasValue)
                estateFile.HasGardenDocument = (bool)model.HasGardenDocument;
            if (model.BoardMountable.HasValue)
                estateFile.BoardMountable = (bool)model.BoardMountable;
            if (model.HasOfficeDocument.HasValue)
                estateFile.HasOfficeDocument = (bool)model.HasOfficeDocument;
            if (model.HasGoodWill.HasValue)
                estateFile.HasGoodWill = (bool)model.HasGoodWill;
            if (model.HasOwnership.HasValue)
                estateFile.HasOwnership = (bool)model.HasOwnership;
            if (model.IsFurnitured.HasValue)
                estateFile.IsFurnitured = (bool)model.IsFurnitured;
            if (model.ToOutsider.HasValue)
                estateFile.ToOutsider = (bool)model.ToOutsider;
            if (model.HasPool.HasValue)
                estateFile.HasPool = (bool)model.HasPool;
            if (model.IsRealEstate.HasValue)
                estateFile.IsRealEstate = (bool)model.IsRealEstate;
            if (model.ResidentOwner.HasValue)
                estateFile.ResidentOwner = (bool)model.ResidentOwner;
            if (model.HasLaundryRoom.HasValue)
                estateFile.HasLaundryRoom = (bool)model.HasLaundryRoom;
            if (model.SeparateSuite.HasValue)
                estateFile.SeparateSuite = (bool)model.SeparateSuite;
            if (model.IntegratedSalon.HasValue)
                estateFile.IntegratedSalon = (bool)model.IntegratedSalon;
            if (model.NeedToRebuild.HasValue)
                estateFile.NeedToRebuild = (bool)model.NeedToRebuild;
            return estateFile;

        }
        public EstateFile AddEstateFile(EstateFileSchema model)
        {
            var file = InitEstateFile(model);
            file = Add(file);
            return file;
        }
        public EstateFile UpdateEstateFile(EstateFileSchema model)
        {
            var file = InitEstateFile(model);
            file = Update(file);
            return file;
        }

        public EstateFileSchema GetEstateFile(long id)
        {
            var file = Get(id);
            var fileSchema = new EstateFileSchema
            {
                AddressStreet = file.AddressStreet,
                AddressAlley = file.AddressAlley,
                AddressNumber = file.AddressNumber,
                Area = file.Area,
                CityID = file.CityID,
                CityName = file.City.CityName,
                CreateByID = file.CreateByID,
                CreateDate = file.CreateDate,
                EstateType = file.EstateType,
                FileCode = file.FileCode,
                FileDescription = file.FileDescription,
                FileID = file.FileID,
                FileType = file.FileType,
                Latitude = file.Latitude,
                Length = file.Length,
                Longtitude = file.Longtitude,
                PersonID = file.PersonID,
                PersonName = file.Person.PersonName,
                ProvinceID = file.ProvinceID,
                ProvinceName = file.Province.ProvinceName,
                RoomCount = file.RoomCount,
                TotalPrice = file.TotalPrice,
                UnitPrice = file.UnitPrice,
                UpdateByID = file.UpdateByID,
                UpdateDate = file.UpdateDate,
                Width = file.Width,
                ZoneID = file.ZoneID,
                BuildingFacade = file.BuildingFacade,
                BuildingFloorCount = file.BuildingFloorCount,
                BuiltUpArea = file.BuiltUpArea,
                Cooling = file.Cooling,
                EstatePhoneNumber = file.EstatePhoneNumber,
                FloorNumber = file.FloorNumber,
                HasBackyard = file.HasBackyard,
                HasBasement = file.HasBasement,
                HasElevator = file.HasElevator,
                HasParking = file.HasParking,
                HasRemoteDoor = file.HasRemoteDoor,
                HasServantRestroom = file.HasServantRestroom,
                HasStoreRoom = file.HasStoreRoom,
                HasVideoDoorPhone = file.HasVideoDoorPhone,
                HasYard = file.HasYard,
                Heating = file.Heating,
                InformationSource = file.InformationSource,
                Kitchen = file.Kitchen,
                OwnershipDocumentStatus = file.OwnershipDocumentStatus,
                RestRoom = file.RestRoom,
                Status = file.Status,

                ParkingCount = file.ParkingCount,
                BuildingAge = file.BuildingAge,
                MasterRoomCount = file.MasterRoomCount,
                StoreRoomArea = file.StoreRoomArea,
                RestRoomCount = file.RestRoomCount,
                UnitCountInFloor = file.UnitCountInFloor,
                ElevatorCount = file.ElevatorCount,
                TerraceArea = file.TerraceArea,
                LobbyCeilingHeight = file.LobbyCeilingHeight,
                UnitCeilingHeight = file.UnitCeilingHeight,
                PassWidth = file.PassWidth,
                YardAread = file.YardAread,
                BasementArea = file.BasementArea,
                FirstFloorArea = file.FirstFloorArea,
                SecondFloorArea = file.SecondFloorArea,
                ThirdFloorArea = file.ThirdFloorArea,
                GroundFloorArea = file.GroundFloorArea,
                DetailedPlanArea = file.DetailedPlanArea,
                KitchenCount = file.KitchenCount,
                BalconyArea = file.BalconyArea,
                AreaForBoard = file.AreaForBoard,
                AllUnitsCount = file.AllUnitsCount,
                AreaForCommerce = file.AreaForCommerce,
                PhoneLineCount = file.PhoneLineCount,
                Flooring = file.Flooring,
                FacadeMaterial = file.FacadeMaterial,
                ElevatorBrand = file.ElevatorBrand,
                ValvesBrand = file.ValvesBrand,
                BlockPieceNumber = file.BlockPieceNumber,
                FloorsArchitecture = file.FloorsArchitecture,
                OneWayTwoWayStreet = file.OneWayTwoWayStreet,
                JobTenure = file.JobTenure,
                AdvContent1 = file.AdvContent1,
                AdvContent2 = file.AdvContent2,
                AdvContent3 = file.AdvContent3,
                HasSauna = file.HasSauna,
                HasJacuzzi = file.HasJacuzzi,
                HasRoofGarden = file.HasRoofGarden,
                HasConferenceHall = file.HasConferenceHall,
                HasLobby = file.HasLobby,
                HasGym = file.HasGym,
                HasCinema = file.HasCinema,
                HasBilliard = file.HasBilliard,
                HasSquash = file.HasSquash,
                HasRoofPool = file.HasRoofPool,
                HasFurnishedLobby = file.HasFurnishedLobby,
                HasEntrance = file.HasEntrance,
                HasLobbyMan = file.HasLobbyMan,
                HasMaidRoom = file.HasMaidRoom,
                HasSeparateSitting = file.HasSeparateSitting,
                HasInteriorPool = file.HasInteriorPool,
                HasCookingKitchen = file.HasCookingKitchen,
                HasFurnishKitchen = file.HasFurnishKitchen,
                HasSemiFurnishKitchen = file.HasSemiFurnishKitchen,
                HasGardenPermission = file.HasGardenPermission,
                HasRooflessPool = file.HasRooflessPool,
                HasBuildingLicense = file.HasBuildingLicense,
                HasPlanCommand = file.HasPlanCommand,
                HasWatchman = file.HasWatchman,
                HasPatio = file.HasPatio,
                HasSeparateStaircase = file.HasSeparateStaircase,
                HasBigTerrace = file.HasBigTerrace,
                HasPanoramaView = file.HasPanoramaView,
                HasSouthView = file.HasSouthView,
                HasNorthView = file.HasNorthView,
                HasWoodyView = file.HasWoodyView,
                HasResidentialStatus = file.HasResidentialStatus,
                HasGardenDocument = file.HasGardenDocument,
                BoardMountable = file.BoardMountable,
                HasOfficeDocument = file.HasOfficeDocument,
                HasGoodWill = file.HasGoodWill,
                HasOwnership = file.HasOwnership,
                IsFurnitured = file.IsFurnitured,
                ToOutsider = file.ToOutsider,
                HasPool = file.HasPool,
                IsRealEstate = file.IsRealEstate,
                ResidentOwner = file.ResidentOwner,
                HasLaundryRoom = file.HasLaundryRoom,
                SeparateSuite = file.SeparateSuite,
                IntegratedSalon = file.IntegratedSalon,
                NeedToRebuild = file.NeedToRebuild,
                Rent = file.Rent,
                Deposit = file.Deposit
            };
            return fileSchema;

        }
        public string GetNextFileCode()
        {
            var maxCode = All().Select(f => f.FileCode).ToList().Max(e => long.Parse(e)) + 1;
            var strCode = "000000" + (maxCode).ToString();
            return strCode.Substring(strCode.Length - 7, 7);
        }
    }
}
