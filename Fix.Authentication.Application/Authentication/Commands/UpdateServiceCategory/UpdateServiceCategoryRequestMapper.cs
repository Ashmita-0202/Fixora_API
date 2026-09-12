namespace Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory
{
    public static class UpdateServiceCategoryRequestMapper
    {
        public static UpdateServiceCategoryCommand ToCommand(this UpdateServiceCategoryRequest request, string id)
        {
            return new UpdateServiceCategoryCommand
            (
                id,
                request.CategoryName,
                request.Description,
                request.IconUrl,
                request.BaseServiceCharge,
                request.EstimatedDuration,
                request.IsActive
            );
        }
    }
}
