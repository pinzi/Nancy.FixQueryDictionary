namespace Nancy.FixQueryDictionaryTest
{
    public class NancyContext
    {
        public NancyContext()
        {

        }
        private Request request;

        /// <summary>
        /// Gets or sets the incoming request
        /// </summary>
        public Request Request
        {
            get
            {
                return this.request;
            }

            set
            {
                this.request = value;
            }
        }
    }
}
