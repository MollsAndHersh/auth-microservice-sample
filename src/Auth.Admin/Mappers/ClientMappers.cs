﻿using Auth.Admin.Models;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;

namespace Auth.Admin.Mappers
{
    public static class ClientMappers
    {
        static ClientMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static ClientModel ToModel(this Client client)
        {
            return Mapper.Map<ClientModel>(client);
        }

        public static Client ToEntity(this ClientModel model)
        {
            return Mapper.Map<Client>(model);
        }

        public static Client ToEntity(this ClientModel model, Client client)
        {
            return Mapper.Map(model, client);
        }
    }
}