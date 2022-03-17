using SeedRoad.Common.Core.Domain.Time;

namespace SeedRoad.Common.Infrastructure.Services;

public class TimeGenerator : ITimeGenerator
{
    public DateTime GetNow()
    {
        return DateTime.Now;
    }
}