using LivestreamingE_commercePlatform.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LivestreamingE_commercePlatform.Permissions
{
    public class LivestreamingE_commercePlatformPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(LivestreamingE_commercePlatformPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(LivestreamingE_commercePlatformPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LivestreamingE_commercePlatformResource>(name);
        }
    }
}
