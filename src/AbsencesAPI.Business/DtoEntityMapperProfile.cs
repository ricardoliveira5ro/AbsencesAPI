﻿using AbsencesAPI.Common.DTOS;
using AbsencesAPI.Common.DTOS.Stats;
using AbsencesAPI.Common.Model;
using AutoMapper;

namespace AbsencesAPI.Business;

public class DtoEntityMapperProfile : Profile
{
	public DtoEntityMapperProfile()
	{
		CreateMap<ManagementCreate, Management>()
			.ForMember(dest => dest.Id, opt => opt.Ignore());
		CreateMap<ManagementUpdate, Management>();
        CreateMap<Management, ManagementGet>();

        CreateMap<StatsCreate, Stats>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<StatsUpdate, Stats>();
        CreateMap<Stats, StatsGet>();
    }
}