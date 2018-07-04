namespace API.MilkteaAdmin.Mapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(x =>
            {
                x.AddProfile<ModelToViewModelProfile>();
                x.AddProfile<ViewModelToModelProfile>();
            });
        }
    }
}