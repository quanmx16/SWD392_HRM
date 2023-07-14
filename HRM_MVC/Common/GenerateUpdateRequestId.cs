namespace HRM_MVC.Common
{
    public class GenerateUpdateRequestId
    {

        public static int GenerateUniqueId()
        {
            var guid = Guid.NewGuid();

            var bytes = guid.ToByteArray();
            var uniqueId = BitConverter.ToInt32(bytes, 0);
            return uniqueId;
        }
    }
}
