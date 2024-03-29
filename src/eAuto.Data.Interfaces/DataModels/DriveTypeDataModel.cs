﻿using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class DriveTypeDataModel
    {
        [Required]
        [MaxLength(50)]
        public int DriveTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get;set;}

        #region Ctor
        public DriveTypeDataModel() { }

        public DriveTypeDataModel(string name)
        {
            Name = name;
        }
		#endregion
	}
}