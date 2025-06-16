using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace Base.BLL;

public class BaseBLL<TUOW> : IBaseBLL
    where TUOW : IBaseUOW
{
    protected readonly TUOW BllUow;

    public BaseBLL(TUOW uow)
    {
        BllUow = uow;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await BllUow.SaveChangesAsync();
    }
}