import { check, group, sleep } from 'k6';
import http from 'k6/http';

export let options = {
  max_vus: 100,
  vus: 100,
  stages: [
    { duration: "30s", target: 10 },
    { duration: "30s", target: 100 },
    { duration: "30s", target: 0 }
  ],
  thresholds: {
    "RTT": ["avg<500"]
  }
}

export default function() {
  group('v1 API testing', function() {
    group('get employees', function() {
        var res = http.get(`${__ENV.BASE_URL}/api/Employees`);
        check(res, { "status is 200": (r) => r.status === 200 });
    });
  });
  sleep(1);
}