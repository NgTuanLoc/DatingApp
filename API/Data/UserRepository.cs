using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class UserRepository(DataContext dataContext, IMapper mapper) : IUserRepository
{
    private readonly DataContext _dataContext = dataContext;
    private readonly IMapper _mapper = mapper;

    public async Task<MemberDto> GetMemberByUsernameAsync(string username)
    {
        return await _dataContext.Users.Where(x => x.UserName == username)
                                        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _dataContext.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _dataContext.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await _dataContext.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _dataContext.Users.Include(p => p.Photos).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
        _dataContext.Entry(user).State = EntityState.Modified;
    }
}