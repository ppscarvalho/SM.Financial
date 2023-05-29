using AutoMapper;
using MediatR;
using SM.Financial.Core.Application.Interfaces.Repositories.Domain;
using SM.Financial.Core.Application.Models;

namespace SM.Financial.Core.Application.Queries.AccountReceivable
{
    public class AccountReceivableQueryHandler :
             IRequestHandler<GetAccountReceivableByIdQuery, AccountReceivableModel>,
             IRequestHandler<GetAllAccountReceivableQuery, IEnumerable<AccountReceivableModel>>
    {
        private readonly IAccountReceivableRepository _accountReceivableRepository;
        private readonly IMapper _mapper;
        public AccountReceivableQueryHandler(IAccountReceivableRepository accountReceivableRepository, IMapper mapper)
        {
            _accountReceivableRepository = accountReceivableRepository;
            _mapper = mapper;
        }

        public async Task<AccountReceivableModel> Handle(GetAccountReceivableByIdQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<AccountReceivableModel>(await _accountReceivableRepository.GetAccountReceivableById(query.Id));
        }

        public async Task<IEnumerable<AccountReceivableModel>> Handle(GetAllAccountReceivableQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<AccountReceivableModel>>(await _accountReceivableRepository.GetAllAccountReceivable());
        }
    }
}
