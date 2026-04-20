using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguesScore.GCommon
{
    public static class ExceptionMessages
    {
        public const string RoleSeedingExceptionMessage = "An error occured while trying to seed the role {0}!";
        public const string AdminAssignExceptionMessage = "Failed to assign admin role to user: {0}!";
        public const string AdminCreationExceptionMessage = "Failed to create admin user: {0}";
    }
}
