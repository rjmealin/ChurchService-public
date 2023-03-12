// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { EnvironmentModel } from './environment.model';

export const environment: EnvironmentModel = {
    production: false,
    apiUrl: 'https://localhost:44353',
    test: false,
    maxFileSize: 2097152,
};
