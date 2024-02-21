using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//بسم الله الرحمن الرحيم
/**
 *
 * @author Huseyin_Aydin
 * @since 1994
 * @category DotNet Core nArchitechture, C#.
 *
 */

namespace Application.Features.Models.Queries.GetListModel;

public class GetListModelQuery : IRequest<ModelListModel>
{
    public PageRequest PageRequest { get; set; }
    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        //private readonly ModelBusinessRules _modelBusinessRules;

        public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper, /*BrandBusinessRules brandBusinessRules*/)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
            //_brandBusinessRules = brandBusinessRules;
        }
        public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await _modelRepository.GetListAsync(include:
                                               m => m.Include(c => c.Brand),
                                               index: request.PageRequest.Page,
                                               size: request.PageRequest.PageSize
                                               );
            //dataModel
            ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
            return mappedModels;
        }
    }
}