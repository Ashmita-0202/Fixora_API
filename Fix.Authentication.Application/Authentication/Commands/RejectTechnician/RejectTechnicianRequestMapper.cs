namespace Fix.Authentication.Application.Authentication.Commands.RejectTechnician
{
    public static class RejectTechnicianRequestMapper
    {
        public static RejectTechnicianCommand ToCommand(this RejectTechnicianRequest request, string profileId)
        {
            return new RejectTechnicianCommand(profileId, request.Reason);
        }
    }
}
