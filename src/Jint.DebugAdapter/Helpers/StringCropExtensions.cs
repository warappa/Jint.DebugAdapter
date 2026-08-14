namespace Jint.DebugAdapter.Helpers;

public static class StringCropExtensions
{
    extension(string value)
    {
        /// <summary>
        /// Crops end of string to make it fit a maximum length, including a separator (e.g. ellipsis).
        /// </summary>
        /// <remarks>
        /// Note that the string is not guaranteed to be cropped to exactly max length, if the string includes
        /// surrogate pairs (32-bit code points). In that case, it may be shorter, in order to not split a
        /// pair.
        /// </remarks>
        public string CropEnd(int maxLength, string ellipsis = "…")
        {
            if (maxLength < ellipsis.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength,
                    $"{nameof(maxLength)} should be >= length of {nameof(ellipsis)} (i.e. {ellipsis.Length}).");
            }

            if (value.Length <= maxLength)
            {
                return value;
            }

            var length = maxLength - ellipsis.Length;
            if (length <= 0)
            {
                return ellipsis;
            }

            if (char.IsSurrogatePair(value, length - 1))
            {
                length--;
            }

            return string.Concat(value.AsSpan(0, length), ellipsis);
        }

        /// <summary>
        /// Crops middle of string to make it fit a maximum length, including a separator (e.g. ellipsis).
        /// </summary>
        /// <remarks>
        /// Note that the string is not guaranteed to be cropped to exactly max length, if the string includes
        /// surrogate pairs (32-bit code points). In that case, it may be shorter, in order to not split a
        /// pair.
        /// </remarks>
        public string CropMiddle(int maxLength, string ellipsis = "…")
        {
            if (maxLength < ellipsis.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength,
                    $"{nameof(maxLength)} should be >= length of {nameof(ellipsis)} (i.e. {ellipsis.Length}).");
            }

            if (value.Length <= maxLength)
            {
                return value;
            }

            if (maxLength == ellipsis.Length)
            {
                return ellipsis;
            }

            maxLength -= ellipsis.Length;
            var leftLength = maxLength / 2;
            var rightLength = maxLength - leftLength;

            // No space for anything except separator (or a single character on one side)
            if (rightLength <= 0 ||
                leftLength <= 0)
            {
                return ellipsis;
            }

            // Ensure we're not splitting surrogate pairs:
            if (char.IsSurrogatePair(value, value.Length - rightLength - 1))
            {
                leftLength++;
                rightLength--;
            }

            if (char.IsSurrogatePair(value, leftLength - 1))
            {
                leftLength--;
            }

            return string.Concat(
                value.AsSpan(0, leftLength),
                ellipsis,
                value.AsSpan(value.Length - rightLength, rightLength)
            );
        }
    }
}
