using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WiserSoft.UI.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Comunicaciones, DATA.Comunicaciones>();
                cfg.CreateMap<DATA.Comunicaciones, Models.Comunicaciones>();
                cfg.CreateMap<Models.Contactos, DATA.Contactos>();
                cfg.CreateMap<DATA.Contactos, Models.Contactos>();
                cfg.CreateMap<Models.Contactos_Por_Listas, DATA.Contactos_Por_Listas>();
                cfg.CreateMap<DATA.Contactos_Por_Listas, Models.Contactos_Por_Listas>();
                cfg.CreateMap<DATA.Difusiones, Models.Difusiones>();
                cfg.CreateMap<Models.Difusiones, DATA.Difusiones>();
                cfg.CreateMap<DATA.Estados, Models.Estados>();
                cfg.CreateMap<Models.Estados, DATA.Estados>();
                cfg.CreateMap<DATA.Historiales, Models.Historiales>();
                cfg.CreateMap<Models.Historiales, DATA.Historiales>();
                cfg.CreateMap<DATA.Listas, Models.Listas>();
                cfg.CreateMap<Models.Listas, DATA.Listas>();
                cfg.CreateMap<DATA.Roles, Models.Roles>();
                cfg.CreateMap<Models.Roles, DATA.Roles>();
                cfg.CreateMap<DATA.Telefonos, Models.Telefonos>();
                cfg.CreateMap < Models.Telefonos, DATA.Telefonos >();
                cfg.CreateMap<DATA.Tipo_Difusiones, Models.Tipo_Difusiones>();
                cfg.CreateMap<Models.Tipo_Difusiones, DATA.Tipo_Difusiones>();
                cfg.CreateMap<Models.Usuarios, DATA.Usuarios>();
                cfg.CreateMap<DATA.Usuarios, Models.Usuarios>();
                cfg.CreateMap<Models.ContactosListas, DATA.ContactosListas>();
                cfg.CreateMap<DATA.ContactosListas, Models.ContactosListas>();
                cfg.CreateMap<Models.Mensajes, DATA.Mensajes>();
                cfg.CreateMap<DATA.Mensajes, Models.Mensajes>();
                cfg.CreateMap<Models.Lista_Negra, DATA.Lista_Negra>();
                cfg.CreateMap<DATA.Lista_Negra, Models.Lista_Negra>();
                cfg.CreateMap<Models.Reporteclientesactivos, DATA.Reporteclientesactivos>();
                cfg.CreateMap<DATA.Reporteclientesactivos, Models.Reporteclientesactivos>();
                cfg.CreateMap<Models.Respuestas, DATA.Respuestas>();
                cfg.CreateMap<DATA.Respuestas, Models.Respuestas>();
            });
        }
    }
}
