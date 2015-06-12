(function () {
    angular.module('app').factory('svc', ['$http', '$q', function ($http, $q) {

        // factory builds a service, the name given here of the service is svc
        // $http and $q are also named here so that we can use them in the service

        var service = {};

        service.getYears = function () {
            var deferred = $q.defer();
            deferred.resolve([1998, 1999, 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007,
                              2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015]);
            return deferred.promise;

        }

        service.getMakes = function (year) {
            var deferred = $q.defer();

            switch (year) {
                case 1998:
                case 1999:
                case 2000:
                    deferred.resolve(['Toyota', 'Honda', 'Ford'])
                    break;
                case 2001:
                case 2002:
                case 2003:
                case 2004:
                    deferred.resolve(['Toyota', 'Honda', 'Ford', 'Acura'])
                    break;
                case 2005:
                case 2006:
                case 2007:
                case 2008:
                case 2009:
                case 2010:
                    deferred.resolve(['Toyota', 'Honda', 'Ford', 'Hyundai', 'Dodge'])
                    break;
                case 2011:
                case 2012:
                case 2013:
                case 2014:
                case 2015:
                    deferred.resolve(['Toyota', 'Honda', 'Ford', 'Hyundai', 'Dodge', 'Acura', 'Nissan'])
                    break;
            }

            return deferred.promise;
        }

        service.getModels = function (year, make) {
            var deferred = $q.defer();

            switch (year) {
                case 1998:
                case 1999:
                case 2000:
                    switch (make) {
                        case 'Toyota': deferred.resolve(['Corolla'])
                            break;
                        case 'Honda': deferred.resolve(['Civic'])
                            break;
                        case 'Ford': deferred.resolve(['Focus'])
                            break;
                    }

                case 2001:
                case 2002:
                case 2003:
                case 2004: switch (make) {
                    case 'Toyota': deferred.resolve(['Camry'])
                        break;
                    case 'Honda': deferred.resolve(['Accord'])
                        break;
                    case 'Ford': deferred.resolve(['Mustang'])
                        break;
                    case 'Acura': deferred.resolve(['Integra'])
                        break;
                }
                case 2005:
                case 2006:
                case 2007:
                case 2008:
                case 2009:
                case 2010: switch (make) {
                    case 'Toyota': deferred.resolve(['RAV4'])
                        break;
                    case 'Honda': deferred.resolve(['CR-V'])
                        break;
                    case 'Ford': deferred.resolve(['F-150'])
                        break;
                    case 'Hyundai': deferred.resolve(['Coupe'])
                        break;
                    case 'Dodge': deferred.resolve(['Durango'])
                        break;
                }
                case 2011:
                case 2012:
                case 2013:
                case 2014:
                case 2015: switch (make) {
                    case 'Toyota': deferred.resolve(['Sienna'])
                        break;
                    case 'Honda': deferred.resolve(['Passport'])
                        break;
                    case 'Ford': deferred.resolve(['Cougar'])
                        break;
                    case 'Hyundai': deferred.resolve(['Coupe'])
                        break;
                    case 'Dodge': deferred.resolve(['Viper'])
                        break;
                    case 'Acura': deferred.resolve(['NSX'])
                        break;
                    case 'Nissan': deferred.resolve(['Sentra'])
                        break;
                }

                    return deferred.promise;
            }
        }
        service.getTrims = function (year, make, model) {
            var deferred = $q.defer();

            switch (year) {
                case 1998:
                case 1999:
                case 2000:
                    switch (make) {
                        case 'Toyota': switch (model) {
                            case 'Corolla': deferred.resolve(['Hatchback'])
                                break;
                        }
                        case 'Honda': switch (model) {
                            case 'Civic': deferred.resolve(['Coupe'])
                                break;
                        }
                        case 'Ford': switch (model) {
                            case 'Focus': deferred.resolve(['1.8 Diesel Estate'])
                                break;
                        }
                    }

                case 2001:
                case 2002:
                case 2003:
                case 2004: switch (make) {
                    case 'Toyota': switch (model) {
                        case 'Camry': deferred.resolve(['Station Wagon'])
                            break;
                    }
                    case 'Honda': switch (model) {
                        case 'Accord': deferred.resolve(['Wagon'])
                            break;
                    }
                    case 'Ford': switch (model) {
                        case 'Mustang': deferred.resolve(['1.4 Zetec SE Estate'])
                            break;
                    }
                    case 'Acura': switch (model) {
                        case 'Integra': deferred.resolve(['GS-R'])
                            break;
                    }
                }
                case 2005:
                case 2006:
                case 2007:
                case 2008:
                case 2009:
                case 2010: switch (make) {
                    case 'Toyota': switch (model) {
                        case 'RAV4': deferred.resolve(['Cabriolet'])
                            break;
                    }
                    case 'Honda': switch (model) {
                        case 'CR-V': deferred.resolve(['2.0i ES'])
                            break;
                    }
                    case 'Ford': switch (model) {
                        case 'F-150': deferred.resolve(['big engine'])
                            break;
                    }
                    case 'Hyundai': switch (model) {
                        case 'Coupe': deferred.resolve(['GS-R'])
                            break;
                    }
                    case 'Dodge': switch (model) {
                        case 'Durango': deferred.resolve(['5.9'])
                            break;
                    }
                }
                case 2011:
                case 2012:
                case 2013:
                case 2014:
                case 2015: switch (make) {
                    case 'Toyota': switch (model) {
                        case 'Sienna': deferred.resolve(['Cabriolet'])
                            break;
                    }
                    case 'Honda': switch (model) {
                        case 'Passport': deferred.resolve(['33333'])
                            break;
                    }
                    case 'Ford': switch (model) {
                        case 'Cougar': deferred.resolve(['small engine'])
                            break;
                    }
                    case 'Hyundai': switch (model) {
                        case 'Coupe': deferred.resolve(['xxx'])
                            break;
                    }
                    case 'Dodge': switch (model) {
                        case 'Viper': deferred.resolve(['yyy'])
                            break;
                    }
                    case 'Acura': switch (model) {
                        case 'NSX': deferred.resolve(['zzz'])
                            break;
                    }
                    case 'Nissan': switch (model) {
                        case 'Sentra': deferred.resolve(['3.0'])
                            break;
                    }
                }

                    return deferred.promise;
            }
        }
        return service;

    }])
})();