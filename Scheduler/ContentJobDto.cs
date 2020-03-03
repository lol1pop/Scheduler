using Scheduler.Entities;
using System;

namespace Scheduler {
    public class ContentJobDto {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; } = DateTime.MaxValue;

        public long ContentId { get; set; }

        public AlertType ContentType { get; set; }

        /// <summary>
        /// Dto for deleting from IContentJobScheduler
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contentType"></param>
        public ContentJobDto(long contentId, AlertType contentType) {
            if (contentId <= 0 || contentType == AlertType.Undefined) {
                throw new ArgumentException();
            }

            ContentId = contentId;
            ContentType = contentType;
        }

        /// <summary>
        ///  Dto for adding to IContentJobScheduler
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="contentId"></param>
        /// <param name="contentType"></param>
        public ContentJobDto(DateTime startDateTime, long contentId, AlertType contentType) {
            if (startDateTime == null) {
                throw new ArgumentException();
            }

            if (contentId <= 0 || contentType == AlertType.Undefined) {
                throw new ArgumentException();
            }

            StartDateTime = startDateTime;
            ContentId = contentId;
            ContentType = contentType;
        }
    }
}