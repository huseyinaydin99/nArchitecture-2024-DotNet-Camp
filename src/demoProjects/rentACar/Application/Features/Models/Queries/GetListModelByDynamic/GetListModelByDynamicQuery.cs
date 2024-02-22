using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListModelByDynamic;

public class GetListModelByDynamicQuery : IRequest<ModelListModel>
{
    public Dynamic Dynamic { get; set; }
    public PageRequest PageRequest { get; set; }
    public class GetListModelByDynamicQueryHandler : IRequestHandler<GetListModelByDynamicQuery, ModelListModel>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        //private readonly ModelBusinessRules _modelBusinessRules;

        public GetListModelByDynamicQueryHandler(IModelRepository modelRepository, IMapper mapper /*,BrandBusinessRules brandBusinessRules*/)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
            //_brandBusinessRules = brandBusinessRules;
        }
        public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(
                                               dynamic: request.Dynamic,
                                               include: m => m.Include(c => c.Brand),
                                               index: request.PageRequest.Page,
                                               size: request.PageRequest.PageSize
                                               );
            //dataModel
            ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
            return mappedModels;
        }
    }
}