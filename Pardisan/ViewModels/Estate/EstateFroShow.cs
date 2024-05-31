using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Pardisan.Models;

namespace Pardisan.ViewModels.Estate
{
    public class EstateFroShow
    {

        public int Id { get; set; }
        //Frist Step
        public string? Address { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        [Display(Name = "متراژ")]
        public string? Meterage { get; set; }
        [Display(Name = "بر")]
        public ProximityStatus? Proximity { get; set; }
        [Display(Name = "متراژ بر")]
        public string? ProximityMeterage { get; set; }
        [Display(Name = "گذر")]
        public string? Passage { get; set; }

        //Second Step
        //--> owners
        public List<int>? Oweners { get; set; }
        //Third Step

        public string? Title { get; set; }
        public string? ProjectSupervisor { get; set; }
        public string? UsageType { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateForShow { get; set; }
        public DateTime ProjectCompletionDate { get; set; }
        public string ProjectCompletionDateForShow { get; set; }
        public ProjectStatus Status { get; set; }
        public string? Description { get; set; }
        public int? Code { get; set; }
        public string? FloorCount { get; set; }
        public float? LandArea { get; set; }
        public float? TotalInfrastructure { get; set; }
        public float? UsefulInfrastructure { get; set; }
        public string? FinancialValue { get; set; }
        public string? City { get; set; }
        public string? EstateMeterage { get; set; }
        public string? UnitInFlorCount { get; set; }
        public string? TotalUnits { get; set; }
        public string? AparatLink { get; set; }
        public int? FloorCounts { get; set; }
        //Fourth Step
        public bool? Fibr { get; set; }
        public bool? AbNama { get; set; }
        public bool? QRCode { get; set; }
        public bool? Camera { get; set; }
        public bool? Security { get; set; }
        public bool? Shomineh { get; set; }
        public bool? Flower { get; set; }
        public bool? Alachigh { get; set; }
        public bool? Showbuilding { get; set; }
        
        public List<EstateFloorDTO>? Floors { get; set; }



        //Fifth Step
        public string? Image { get; set; }
        public ICollection<EstateImagesDTO>? Images { get; set; }



        //Sixth Step
        //--> Project Progress
        public ProjectProgress ProjectProgress { get; set; }
        public List<FullEstateProgressDTO> EstateProgresses { get; set; }
        //Seventh Step
        public bool IsPublish { get; set; }



    }
    public enum ProximityStatus
    {
        [Display(Name = "تک بر")]
        ProximityOne,
        [Display(Name = "دو بر")]
        ProximityTwo,
        [Display(Name = "سه بر")]
        ProximityThree,
        [Display(Name = "چهار بر")]
        ProximityFour,

    }
    public class EstateFloorDTO
    {
        public bool IsActive { get; set; }
        public int Id { get; set; }
        public int? FloorNumber { get; set; }
        public string? FloorName { get; set; }
        public int? ClientId { get; set; }
        public List<EstateUnitDTO>? EstateUnits { get; set; }
    }
    public class EstateUnitDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? MergedUnitId { get; set; }
        public bool IsEmptyUnit { get; set; }
        public int? ClientIdForUnit { get; set; }
        public int? FloorNumber { get; set; }
        public int? Meterage { get; set; }
        public List<int>? OwnerId { get; set; }
    }
    public enum ProjectProgress
    {
        [Display(Name = "پی ریزی")]
        initial,
        [Display(Name = "ستون")]
        structure,
        [Display(Name = "دیوار چینی")]
        Walls,
        [Display(Name = "گچ کاری")]
        Plastering,
        [Display(Name = "تاسیسات")]
        facilities,
        [Display(Name = "گاشی کاری")]
        Tiling,

    }
    public class FullEstateProgressDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? EstateId { get; set; }
        public ProjectProgress ProjectProgress { get; set; }
        public string? Description { get; set; }
        public DateTime? ProgressDate { get; set; }
        public string? Image { get; set; }
        public ICollection<EstateProgressGalleryDTO>? Images { get; set; }
    }
    public class EstateProgressGalleryDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
    public class EstateImagesDTO
    {
        public int? Id { get; set; }
        public string? Url { get; set; }
        public string? VideoUrl { get; set; }
    }
}
