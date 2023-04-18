using Microsoft.AspNetCore.Mvc;

namespace TrananAPI.Interface;

public interface IController<T1, T2>
    where T1 : IDTO
    where T2 : IDTO
{
    public Task<ActionResult<IEnumerable<T1>>> Get();
    public Task<ActionResult<T1>> GetById(int id);
    public Task<ActionResult<T1>> Post(T2 obj);
    public Task<ActionResult<T1>> Put(T2 obj);
    public Task<IActionResult> DeleteById(int id);
}
