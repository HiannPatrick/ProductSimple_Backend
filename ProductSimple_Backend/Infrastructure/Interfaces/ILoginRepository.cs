using ProductSimple_Backend.Application;
using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Infrastructure
{
	public interface ILoginRepository
	{
		Task<ReturnCommon> Authenticate( LoginDto login );
	}
}
