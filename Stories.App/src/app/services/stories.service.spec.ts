import { TestBed } from '@angular/core/testing';

import { StoriesService } from './stories.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { Story } from '../models/story.type';
 
describe('StoriesService', () => {
  let service: StoriesService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({ imports: [HttpClientTestingModule], providers: [StoriesService]});
    service = TestBed.inject(StoriesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  })

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('getStories should return', () => {
    const request = {orderBy: 'priority', limit: 50}
    let response: Story[] = [{
      title: 'title',
      url: 'url'
    },
    {
      title: 'title2',
      url: 'url'
    }];

    service.getStories(request.orderBy, request.limit).subscribe(data =>{
      expect(data).toEqual(response);
    });

    const req = httpMock.expectOne(`https://localhost:7236/stories-api/Story/GetStories?OrderBy=${request.orderBy}&Limit=${request.limit}`);
    expect(req.request.method).toBe('GET');
    req.flush(response);
  });

});
