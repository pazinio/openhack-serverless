using System;
using Newtonsoft.Json;

namespace Openhack.Functions
{
    public class RatingItem
    {
        public Guid id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public string Rating { get; set; }
        public string UserNotes { get; set; }

        public RatingItem(string UserId, string ProductId, string LocationName, string Rating, string UserNotes)
        {
            this.id = Guid.NewGuid();
            this.Timestamp = DateTime.UtcNow;
            this.UserId = UserId;
            this.ProductId = ProductId;
            this.LocationName = LocationName;
            this.Rating = Rating;
            this.UserNotes = UserNotes;
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }

        public static RatingItem generate(dynamic data) {
            string userId = data?.userId;
            string productId = data?.productId;
            string locationName = data?.locationName;
            string rating = data?.rating;
            string userNotes = data?.userNotes;
            return new RatingItem(userId, productId, locationName, rating, userNotes);
        }
    }
}

