using System;
using AutoMapper;

namespace ToDoListApp
{
    public class AutoMapperConfiguration
    {
        public static void Execute(Type[] profiles = null)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultProfile>();

                if (profiles != null)
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                }
            });
        }
    }

    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {

        }
    }
}