using Microsoft.AspNetCore.Components.Forms;
using SharedLib.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib;

public class Photo
{
    public int Id { get; set; }

    [Required]
    public string PhotoName { get; set; }

    [Required]
    public byte[] PhotoData { get; set; }

    public ICollection<TravelPackage> TravelPackages { get; set; } = new List<TravelPackage>();

    public static async Task<byte[]> CreateByteArrayFromFile(IBrowserFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        byte[] fileBytes = memoryStream.ToArray();
        return fileBytes;
    }

    public string PreviewImage()
    {
        if (PhotoData == null || PhotoData.Length == 0)
            return string.Empty;
        return $"data:image/jpeg;base64,{Convert.ToBase64String(PhotoData)}";
    }
}