using Microsoft.AspNetCore.Components.Forms;
using SharedLib.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLib;

public partial class Photo
{
    public int Id { get; set; }

    [Required]
    public string PhotoName { get; set; }

    [Required]
    public byte[] PhotoData { get; set; }

    

    public static async Task<byte[]> CreateByteArrayFromFile(IBrowserFile file)
    {
        const long maxFileSize = 1024 * 1024 * 15;
        if (file.Size > maxFileSize)
        {
            throw new InvalidOperationException($"File size exceeds the maximum limit of {maxFileSize / (1024 * 1024)} MB.");
        }
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file), "File cannot be null.");
        }

        using var stream = file.OpenReadStream(maxFileSize);
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public string PreviewImage()
    {
        if (PhotoData == null || PhotoData.Length == 0)
            return string.Empty;
        return $"data:image/jpeg;base64,{Convert.ToBase64String(PhotoData)}";
    }

}

public partial class Photo
{
    [JsonIgnore]
    public virtual ICollection<TravelPackage> TravelPackages { get; set; } = new List<TravelPackage>();
}