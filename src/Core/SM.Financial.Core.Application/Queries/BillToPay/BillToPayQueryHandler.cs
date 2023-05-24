using AutoMapper;
using MediatR;
using SM.Financial.Core.Application.Interfaces.Repositories.Domain;
using SM.Financial.Core.Application.Models;

namespace SM.Financial.Core.Application.Queries.BillToPay
{
    public class BillToPayQueryHandler :
            IRequestHandler<GetBillToPayByIdQuery, BillToPayModel>,
            IRequestHandler<GetAllBillToPayQuery, IEnumerable<BillToPayModel>>
    {
        private readonly IBillToPayRepository _BillToPayRepository;
        private readonly IMapper _mapper;
        public BillToPayQueryHandler(IBillToPayRepository BillToPayRepository, IMapper mapper)
        {
            _BillToPayRepository = BillToPayRepository;
            _mapper = mapper;
        }

        public async Task<BillToPayModel> Handle(GetBillToPayByIdQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<BillToPayModel>(await _BillToPayRepository.GetBillToPayById(query.Id));
        }

        public async Task<IEnumerable<BillToPayModel>> Handle(GetAllBillToPayQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<BillToPayModel>>(await _BillToPayRepository.GetAllBillToPay());
        }
    }
}
