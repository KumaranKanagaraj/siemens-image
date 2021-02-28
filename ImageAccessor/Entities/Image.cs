using System;

namespace ImageAccessor.Entities
{
	public class Image
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Content { get; set; }
		public DateTime CreatedOn { get; set; }
	}

    public enum Type
	{
        jpg,
        jpeg,
        bmp,
        png
    }
}
