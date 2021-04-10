namespace MyTestProjectDomainLayer.BaseClasses
{
    /// <summary>
    /// Common Wrapper for All API response.
    /// </summary>
	public class ApiResponseWrapper
	{
        /// <summary>
        /// Status will be boolean. In case of response code 200/201 it will be true
        /// </summary>
		public bool apiResponseStatus { get; set; }

        /// <summary>
        /// Meaningful message in case of failure or success.
        /// </summary>
		public string apiResponseMessage { get; set; }

        public string apiResponseCode { get; set; }

        /// <summary>
        /// Response data.
        /// </summary>
		public dynamic apiResponseData { get; set; }

        /// <summary>
        /// Set it true to force logout user.
        /// </summary>
        public bool forceLogout { get; set; }

        private void InitializeObject()
        {
            apiResponseStatus = false;
            apiResponseMessage = string.Empty;
            apiResponseData = new dynamic[0];
            forceLogout = false;
        }
        public ApiResponseWrapper()
		{
            this.InitializeObject();

        }
	}
}
