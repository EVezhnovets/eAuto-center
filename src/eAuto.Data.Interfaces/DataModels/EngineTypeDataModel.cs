﻿using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class EngineTypeDataModel
	{
        [Required]
        [MaxLength(50)]
        public int EngineTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get;set;}

        #region Ctor
        public EngineTypeDataModel() { }
        public EngineTypeDataModel(string name)
        {
            Name = name;
        }
		#endregion
	}
}